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

namespace backend.Application.UseCases.Sale.Commands.UpdateCommand;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSaleHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existSale = await _unitOfWork.Sale.GetByIdAsync(request.SaleId);
            if (existSale is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var appointment = _mapper.Map<Entity.Sale>(request);
            appointment.Id = request.SaleId;

            var parameters = new DynamicParameters();
            parameters.Add("pSaleId", appointment.Id, DbType.Int32);
            parameters.Add("pClientId", request.ClientId, DbType.Int32);
            parameters.Add("pSaleDate", request.SaleDate, DbType.DateTime);
            parameters.Add("pState", request.State, DbType.Int32);
            parameters.Add("pAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Sale.ExecAsync(SP.SpUpdateSale, parameters);

            if (!result)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
                return response;
            }

            foreach (var detail in request.SaleDetail)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("P_SaleId", request.SaleId, DbType.Int32);
                detailParams.Add("P_ProductId", detail.ProductId, DbType.Int32);
                detailParams.Add("P_Quantity", detail.Quantity, DbType.Int32);
                detailParams.Add("P_Price", detail.Price, DbType.Int32);
                detailParams.Add("P_State", request.State, DbType.Int32);
                detailParams.Add("P_AuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

                await _unitOfWork.SaleDetail.ExecAsync(SP.SpUpdateSaleDetail, detailParams);
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