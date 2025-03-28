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

namespace backend.Application.UseCases.Vaccine.Commands.DeleteCommand;

public class DeleteVaccineHandler : IRequestHandler<DeleteVaccineCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVaccineHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteVaccineCommand request, CancellationToken cancellationToken)
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

            var parameters = new DynamicParameters();
            parameters.Add("P_AuditDeleteUser", request.AuditDeleteUser);
            parameters.Add("P_VaccineId", request.VaccineId);

            var result = await _unitOfWork.Vaccine.ExecAsync(SP.SpDeleteVaccine, parameters);

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