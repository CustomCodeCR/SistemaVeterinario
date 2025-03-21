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
using BC = BCrypt.Net.BCrypt;
using Entity = backend.Domain.Entities;

namespace backend.Application.UseCases.User.Commands.UpdateCommand;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existUser = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (existUser is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var user = _mapper.Map<Entity.Appuser>(request);
            user.Id = request.UserId;

            if (request.Password is not null)
                user.Password = BC.HashPassword(request.Password);
            else
                user.Password = existUser.Password;

            var parameters = new DynamicParameters();
            parameters.Add("PUserId", request.UserId, DbType.Int32);
            parameters.Add("PFirstName", user.Firstname, DbType.String);
            parameters.Add("PLastName", user.Lastname, DbType.String);
            parameters.Add("PUsername", user.Username, DbType.String);
            parameters.Add("PPassword", user.Password, DbType.String);
            parameters.Add("PEmail", user.Email, DbType.String);
            parameters.Add("PUserType", user.Usertype, DbType.String);
            parameters.Add("PState", user.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.User.ExecAsync(SP.SpUpdateAppUser, parameters);

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