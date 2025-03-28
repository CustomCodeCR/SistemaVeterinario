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

namespace backend.Application.UseCases.Vaccine.Commands.UpdateCommand;

public class UpdateVaccineHandler : IRequestHandler<UpdateVaccineCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateVaccineHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateVaccineCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existVaccine = await _unitOfWork.Vaccine.GetByIdAsync(request.VaccineId);

            if (existVaccine is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var client = _mapper.Map<Entity.Vaccine>(request);
            client.Id = request.VaccineId;

            var parameters = new DynamicParameters();
            parameters.Add("P_VaccineId", request.VaccineId, DbType.Int32);
            parameters.Add("P_VaccineName", client.Vaccinename, DbType.String);
            parameters.Add("P_Description", client.Description, DbType.String);
            parameters.Add("P_Type", client.Type, DbType.String);
            parameters.Add("P_State", client.State, DbType.Int32);
            parameters.Add("P_AuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Vaccine.ExecAsync(SP.SpUpdateVaccine, parameters);

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