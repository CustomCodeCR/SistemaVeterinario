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

namespace backend.Application.UseCases.Vaccine.Commands.CreateCommand;

public class CreateVaccineHandler : IRequestHandler<CreateVaccineCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateVaccineHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var client = _mapper.Map<Entity.Vaccine>(request);

            var parameters = new DynamicParameters();
            parameters.Add("P_VaccineName", client.Vaccinename, DbType.String);
            parameters.Add("P_Description", client.Description, DbType.String);
            parameters.Add("P_Type", client.Type, DbType.String);
            parameters.Add("P_State", client.State, DbType.Int32);
            parameters.Add("P_AuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("P_VaccineId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.User.ExecAsync(SP.SpCreateVaccine, parameters);

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