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

namespace backend.Application.UseCases.Pet.Commands.UpdateCommand;

public class UpdatePetHandler : IRequestHandler<UpdatePetCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdatePetHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
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

            var pet = _mapper.Map<Entity.Pet>(request);
            pet.Id = request.PetId;

            var parameters = new DynamicParameters();
            parameters.Add("PPetId", request.PetId, DbType.Int32);
            parameters.Add("PClientId", pet.Clientid, DbType.String);
            parameters.Add("PName", pet.Name, DbType.String);
            parameters.Add("PType", pet.Type, DbType.String);
            parameters.Add("PBreed", pet.Breed, DbType.String);
            parameters.Add("PAge", pet.State, DbType.Int32);
            parameters.Add("PState", pet.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Pet.ExecAsync(SP.SpUpdatePet, parameters);

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