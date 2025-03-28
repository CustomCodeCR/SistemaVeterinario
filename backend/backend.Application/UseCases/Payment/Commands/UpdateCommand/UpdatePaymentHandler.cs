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

namespace backend.Application.UseCases.Payment.Commands.UpdateCommand;

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePaymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
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

            var payment = _mapper.Map<Entity.Payment>(request);
            payment.Id = request.PaymentId;

            var parameters = new DynamicParameters();
            parameters.Add("PSaleId", request.PaymentId, DbType.Int32);
            parameters.Add("PAmount", request.UserId, DbType.Int32);
            parameters.Add("PPaymentDate", payment.Address, DbType.String);
            parameters.Add("PPaymentType", payment.Phone, DbType.String);
            parameters.Add("PState", payment.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Payment.ExecAsync(SP.SpUpdatePayment, parameters);

            if (result)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_FAILED;
            }
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