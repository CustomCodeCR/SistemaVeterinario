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

namespace backend.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = _mapper.Map<Entity.Appuser>(request);
            user.Password = BC.HashPassword(user.Password);

            var parameters = new DynamicParameters();
            parameters.Add("PFirstName", user.Firstname, DbType.String);
            parameters.Add("PLastName", user.Lastname, DbType.String);
            parameters.Add("PUsername", user.Username, DbType.String);
            parameters.Add("PPassword", user.Password, DbType.String);
            parameters.Add("PEmail", user.Email, DbType.String);
            parameters.Add("PUserType", user.Usertype, DbType.String);
            parameters.Add("PState", user.State, DbType.Int32);
            parameters.Add("PAuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("PUserId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.User.ExecAsync(SP.SpCreateAppUser, parameters);

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