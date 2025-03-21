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

namespace backend.Application.UseCases.Client.Commands.UpdateCommand;

public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateClientHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existClient = await _unitOfWork.Client.GetByIdAsync(request.ClientId);

            if (existClient is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var client = _mapper.Map<Entity.Client>(request);
            client.Id = request.ClientId;

            var parameters = new DynamicParameters();
            parameters.Add("PClientId", request.ClientId, DbType.Int32);
            parameters.Add("PUserId", request.UserId, DbType.Int32);
            parameters.Add("PAddress", client.Address, DbType.String);
            parameters.Add("PPhone", client.Phone, DbType.String);
            parameters.Add("PState", client.State, DbType.Int32);
            parameters.Add("PAuditUpdateClient", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Client.ExecAsync(SP.SpUpdateClient, parameters);

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