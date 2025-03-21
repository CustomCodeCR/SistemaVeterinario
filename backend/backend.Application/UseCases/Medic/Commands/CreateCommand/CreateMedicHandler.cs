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

namespace backend.Application.UseCases.Medic.Commands.CreateCommand;

public class CreateMedicHandler : IRequestHandler<CreateMedicCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMedicHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateMedicCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var client = _mapper.Map<Entity.Medic>(request);

            var parameters = new DynamicParameters();
            parameters.Add("PUserId", client.Userid, DbType.String);
            parameters.Add("PSpecialty", client.Specialty, DbType.String);
            parameters.Add("PPhone", client.Phone, DbType.String);
            parameters.Add("PState", client.State, DbType.Int32);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("PMedicId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.User.ExecAsync(SP.SpCreateMedic, parameters);

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