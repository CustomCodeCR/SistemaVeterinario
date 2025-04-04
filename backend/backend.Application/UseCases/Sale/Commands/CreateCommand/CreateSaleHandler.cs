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

namespace backend.Application.UseCases.Sale.Commands.CreateCommand;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("pClientId", request.ClientId, DbType.Int32);
            parameters.Add("pSaleDate", request.SaleDate, DbType.DateTime);
            parameters.Add("pState", request.State, DbType.Int32);
            parameters.Add("pAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("pSaleId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Sale.ExecAsync(SP.SpCreateSale, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            var saleId = parameters.Get<int>("pSaleId");

            foreach (var detail in request.SaleDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("P_SaleId", saleId, DbType.Int32);
                detailParams.Add("P_ProductId", detail.ProductId, DbType.Int32);
                detailParams.Add("P_Quantity", detail.Quantity, DbType.Int32);
                detailParams.Add("P_Price", detail.Price, DbType.Int32);
                detailParams.Add("P_State", request.State, DbType.Int32);
                detailParams.Add("P_AuditCreateUser", request.AuditCreateUser, DbType.Int32);

                await _unitOfWork.SaleDetail.ExecAsync(SP.SpCreateSaleDetail, detailParams);
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