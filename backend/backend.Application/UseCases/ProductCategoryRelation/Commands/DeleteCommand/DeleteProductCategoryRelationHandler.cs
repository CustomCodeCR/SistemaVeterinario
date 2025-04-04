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

namespace backend.Application.UseCases.ProductCategoryRelation.Commands.DeleteCommand;

public class DeleteProductCategoryRelationHandler : IRequestHandler<DeleteProductCategoryRelationCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCategoryRelationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteProductCategoryRelationCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("PAuditDeleteUser", request.AuditDeleteUser);
            parameters.Add("PProductId", request.ProductId);
            parameters.Add("PCategoryId", request.CategoryId);

            var result = await _unitOfWork.ProductCategoryRelation.ExecAsync(SP.SpDeleteProductCategoryRelation, parameters);

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