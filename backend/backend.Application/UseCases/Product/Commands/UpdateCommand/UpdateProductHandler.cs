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

namespace backend.Application.UseCases.Product.Commands.UpdateCommand;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existProduct = await _unitOfWork.Product.GetByIdAsync(request.ProductId);

            if (existProduct is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var client = _mapper.Map<Entity.Product>(request);
            client.Id = request.ProductId;

            if (request.Image is not null)
                client.Image = await _fileStorageService.EditFile(Containers.PRODUCT, request.Image, existProduct.Image!);
            else
                client.Image = existProduct.Image;

            var parameters = new DynamicParameters();
            parameters.Add("PProductId", request.ProductId, DbType.Int32);
            parameters.Add("PName", client.Name, DbType.String);
            parameters.Add("PImage", client.Image, DbType.String);
            parameters.Add("PDescription", client.Description, DbType.String);
            parameters.Add("PPrice", client.Price, DbType.Int32);
            parameters.Add("PState", client.State, DbType.Int32);
            parameters.Add("PAuditUpdateUser", request.AuditUpdateUser, DbType.Int32);

            var result = await _unitOfWork.Product.ExecAsync(SP.SpUpdateProduct, parameters);

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