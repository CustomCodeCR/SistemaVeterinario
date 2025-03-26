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

namespace backend.Application.UseCases.Supplier.Commands.CreateCommand;

public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var supplier = _mapper.Map<Entity.Supplier>(request);

            var parameters = new DynamicParameters();
            parameters.Add("P_Name", supplier.Name, DbType.String);
            parameters.Add("P_Contact", supplier.Contact, DbType.String);
            parameters.Add("P_Address", supplier.Address, DbType.String);
            parameters.Add("P_Phone", supplier.Phone, DbType.String);
            parameters.Add("P_State", supplier.State, DbType.Int32);
            parameters.Add("P_AuditCreateUser", request.AuditCreateUser, DbType.Int32);
            parameters.Add("P_SupplierId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.User.ExecAsync(SP.SpCreateSupplier, parameters);

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