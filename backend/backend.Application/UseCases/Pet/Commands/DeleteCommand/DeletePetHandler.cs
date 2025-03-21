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

namespace backend.Application.UseCases.Pet.Commands.DeleteCommand;

public class DeletePetHandler : IRequestHandler<DeletePetCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeletePetCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existPet = await _unitOfWork.Pet.GetByIdAsync(request.PetId);

            if (existPet is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var parameters = new DynamicParameters();
            parameters.Add("PAuditDeletePet", request.AuditDeleteUser);
            parameters.Add("PPetId", request.PetId);

            var result = await _unitOfWork.Pet.ExecAsync(SP.SpDeletePet, parameters);

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