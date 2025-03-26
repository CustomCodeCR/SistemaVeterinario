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

namespace backend.Application.UseCases.Supplier.Commands.UpdateCommand;

public class UpdateSupplierHandler : IRequestHandler<UpdateSupplierCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existSupplier = await _unitOfWork.Supplier.GetByIdAsync(request.SupplierId);

            if (existSupplier is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var supplier = _mapper.Map<Entity.Supplier>(request);
            supplier.Id = request.SupplierId;

            var parameters = new DynamicParameters();
            parameters.Add("P_SupplierId", request.SupplierId, DbType.Int32);
            parameters.Add("P_Name", supplier.Name, DbType.String);
            parameters.Add("P_Contact", supplier.Contact, DbType.String);
            parameters.Add("P_Address", supplier.Address, DbType.String);
            parameters.Add("P_Phone", supplier.Phone, DbType.String);
            parameters.Add("P_State", supplier.State, DbType.Int32);
            parameters.Add("P_AuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Supplier.ExecAsync(SP.SpUpdateSupplier, parameters);

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