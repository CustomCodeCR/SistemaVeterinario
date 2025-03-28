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

namespace backend.Application.UseCases.Payment.Commands.DeleteCommand;

public class DeletePaymentHandler : IRequestHandler<DeletePaymentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePaymentHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existPayment = await _unitOfWork.Payment.GetByIdAsync(request.PaymentId);

            if (existPayment is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var parameters = new DynamicParameters();
            parameters.Add("PAuditDeleteUser", request.AuditDeleteUser);
            parameters.Add("PPaymentId", request.PaymentId);

            var result = await _unitOfWork.Payment.ExecAsync(SP.SpDeletePayment, parameters);

            if (result)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}