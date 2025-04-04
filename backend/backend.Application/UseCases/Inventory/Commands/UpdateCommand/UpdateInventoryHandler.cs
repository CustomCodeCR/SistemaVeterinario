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

namespace backend.Application.UseCases.Inventory.Commands.UpdateCommand;

public class UpdateInventoryHandler : IRequestHandler<UpdateInventoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInventoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existInventory = await _unitOfWork.Inventory.GetByIdAsync(request.InventoryId);

            if (existInventory is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var Inventory = _mapper.Map<Entity.Inventory>(request);
            Inventory.Id = request.InventoryId;

            var parameters = new DynamicParameters();
            parameters.Add("PInventoryId", request.InventoryId, DbType.Int32);
            parameters.Add("PProductId", request.ProductId, DbType.Int32);
            parameters.Add("PQuantity", request.Quantity, DbType.Int32);
            parameters.Add("PUpdateDate", Inventory.Updatedate, DbType.Date);
            parameters.Add("PState", Inventory.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Inventory.ExecAsync(SP.SpUpdateInventory, parameters);

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