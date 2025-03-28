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

namespace backend.Application.UseCases.Payment.Commands.CreateCommand;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePaymentHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var payment = _mapper.Map<Entity.Payment>(request);

            var parameters = new DynamicParameters();
            parameters.Add("PSaleId", payment.Userid, DbType.int);
            parameters.Add("PAmount", payment.Address, DbType.int);
            parameters.Add("PPaymentDate", payment.Phone, DbType.DateTime);
            parameters.Add("PPaymentType", payment.State, DbType.String);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("PPaymentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.User.ExecAsync(SP.SpCreatePayment, parameters);

            if (result)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessage.MESSAGE_SAVE;
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