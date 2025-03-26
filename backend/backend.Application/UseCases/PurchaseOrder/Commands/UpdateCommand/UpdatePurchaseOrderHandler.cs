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
using Entity = backend.Domain.Entities;

namespace backend.Application.UseCases.PurchaseOrder.Commands.UpdateCommand;

public class UpdatePurchaseOrderHandler : IRequestHandler<UpdatePurchaseOrderCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePurchaseOrderHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existPurchaseOrder = await _unitOfWork.PurchaseOrder.GetByIdAsync(request.PurchaseOrderId);
            if (existPurchaseOrder is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointment = _mapper.Map<Entity.Purchaseorder>(request);
            appointment.Id = request.PurchaseOrderId;

            var parameters = new DynamicParameters();
            parameters.Add("PPurchaseOrderId", appointment.Id, DbType.Int32);
            parameters.Add("PSupplierId", request.SupplierId, DbType.Int32);
            parameters.Add("POrderDate", request.OrderDate, DbType.DateTime);
            parameters.Add("PStatus", request.Status, DbType.String);
            parameters.Add("PState", request.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.PurchaseOrder.ExecAsync(SP.SpUpdatePurchaseOrder, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            foreach (var detail in request.PurchaseOrderDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PPurchaseOrderId", request.PurchaseOrderId, DbType.Int32);
                detailParams.Add("PProductId", detail.ProductId, DbType.Int32);
                detailParams.Add("PQuantity", detail.Quantity, DbType.Int32);
                detailParams.Add("PUnitPrice", detail.UnitPrice, DbType.Int32);
                detailParams.Add("PState", request.State, DbType.Int32);
                detailParams.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

                await _unitOfWork.PurchaseOrderDetail.ExecAsync(SP.SpUpdatePurchaseOrderDetail, detailParams);
            }

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
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