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

namespace backend.Application.UseCases.AppliedVaccine.Commands.UpdateCommand;

public class UpdateAppliedVaccineHandler : IRequestHandler<UpdateAppliedVaccineCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAppliedVaccineHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateAppliedVaccineCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existAppliedVaccine = await _unitOfWork.AppliedVaccine.GetByIdAsync(request.AppliedVaccineId);

            if (existAppliedVaccine is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var AppliedVaccine = _mapper.Map<Entity.AppliedVaccine>(request);
            AppliedVaccine.Id = request.AppliedVaccineId;

            var parameters = new DynamicParameters();
            parameters.Add("PAppliedVaccineId", request.AppliedVaccineId, DbType.Int32);
            parameters.Add("PPetId ", client.Petid, DbType.String);
            parameters.Add("PVaccineId", client.Vaccineid, DbType.Int32);
            parameters.Add("PState", client.State, DbType.Int32);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("AVAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.AppliedVaccine.ExecAsync(SP.SpUpdateAppliedVaccine, parameters);

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