// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using Dapper;
using MediatR;
using System.Data;
using WatchDog;

namespace backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;

public class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePurchaseOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("PSupplierId", request.SupplierId, DbType.Int32);
            parameters.Add("POrderDate", request.OrderDate, DbType.DateTime);
            parameters.Add("PStatus", request.Status, DbType.String);
            parameters.Add("PState", request.State, DbType.Int32);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("PPurchaseOrderId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.PurchaseOrder.ExecAsync(SP.SpCreatePurchaseOrder, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            var purchaseOrderId = parameters.Get<int>("PPurchaseOrderId");

            foreach (var detail in request.PurchaseOrderDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PPurchaseOrderId", purchaseOrderId, DbType.Int32);
                detailParams.Add("PProductId", detail.ProductId, DbType.Int32);
                detailParams.Add("PQuantity", detail.Quantity, DbType.Int32);
                detailParams.Add("PUnitPrice", detail.UnitPrice, DbType.Int32);
                detailParams.Add("PState", request.State, DbType.Int32);
                detailParams.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);

                await _unitOfWork.PurchaseOrderDetail.ExecAsync(SP.SpCreatePurchaseOrderDetail, detailParams);
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}