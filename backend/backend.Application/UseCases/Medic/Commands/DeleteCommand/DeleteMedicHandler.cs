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

namespace backend.Application.UseCases.Medic.Commands.DeleteCommand;

public class DeleteMedicHandler : IRequestHandler<DeleteMedicCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMedicHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteMedicCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existMedic = await _unitOfWork.Medic.GetByIdAsync(request.MedicId);

            if (existMedic is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var parameters = new DynamicParameters();
            parameters.Add("PAuditDeleteMedic", request.AuditDeleteUser);
            parameters.Add("PMedicId", request.MedicId);

            var result = await _unitOfWork.Medic.ExecAsync(SP.SpDeleteMedic, parameters);

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