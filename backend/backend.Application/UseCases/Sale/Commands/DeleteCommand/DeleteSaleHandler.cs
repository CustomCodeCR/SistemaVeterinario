// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using Dapper;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.Sale.Commands.DeleteCommand;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSaleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
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

            var appointmentDetails = await _unitOfWork.SaleDetail.GetSaleDetailBySaleId(request.SaleId);

            foreach (var detail in appointmentDetails)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("P_SaleId", request.SaleId);
                detailParams.Add("P_ProductId", detail.Productid);
                detailParams.Add("P_AuditDeleteUser", request.AuditDeleteUser);

                await _unitOfWork.SaleDetail.ExecAsync(SP.SpDeleteSaleDetail, detailParams);
            }

            var appointmentParams = new DynamicParameters();
            appointmentParams.Add("pAuditDeleteUser", request.AuditDeleteUser);
            appointmentParams.Add("pSaleId", request.SaleId);

            var result = await _unitOfWork.Sale.ExecAsync(SP.SpDeleteSale, appointmentParams);

            response.IsSuccess = result;
            response.Message = result ? ReplyMessage.MESSAGE_DELETE : ReplyMessage.MESSAGE_FAILED;
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