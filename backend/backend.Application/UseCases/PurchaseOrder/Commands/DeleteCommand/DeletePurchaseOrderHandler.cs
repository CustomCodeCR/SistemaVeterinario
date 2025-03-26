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

namespace backend.Application.UseCases.PurchaseOrder.Commands.DeleteCommand;

public class DeletePurchaseOrderHandler : IRequestHandler<DeletePurchaseOrderCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePurchaseOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
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

            var appointmentDetails = await _unitOfWork.PurchaseOrderDetail.GetPurchaseOrderDetailByPurchaseOrderId(request.PurchaseOrderId);

            foreach (var detail in appointmentDetails)
            {
                var detailParams = new DynamicParameters();
                detailParams.Add("PPurchaseOrderId", request.PurchaseOrderId);
                detailParams.Add("PProductId", detail.Productid);
                detailParams.Add("PAuditDeleteUser", request.AuditDeleteUser);

                await _unitOfWork.PurchaseOrderDetail.ExecAsync(SP.SpDeletePurchaseOrderDetail, detailParams);
            }

            var appointmentParams = new DynamicParameters();
            appointmentParams.Add("PAuditDeleteUser", request.AuditDeleteUser);
            appointmentParams.Add("PPurchaseOrderId", request.PurchaseOrderId);

            var result = await _unitOfWork.PurchaseOrder.ExecAsync(SP.SpDeletePurchaseOrder, appointmentParams);

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