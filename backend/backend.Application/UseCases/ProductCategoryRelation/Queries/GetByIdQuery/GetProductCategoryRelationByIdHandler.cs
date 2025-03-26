// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------

using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategoryRelation.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.ProductCategoryRelation.Queries.GetByIdQuery;

public class GetProductCategoryRelationByIdHandler : IRequestHandler<GetProductCategoryRelationByIdQuery, BaseResponse<ProductCategoryRelationByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductCategoryRelationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductCategoryRelationByIdResponseDto>> Handle(GetProductCategoryRelationByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ProductCategoryRelationByIdResponseDto>();

        try
        {
            var ProductCategoryRelation = await _unitOfWork.ProductCategoryRelation.GetByIdAsync(request.ProductCategoryRelationId);

            if (ProductCategoryRelation is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<ProductCategoryRelationByIdResponseDto>(ProductCategoryRelation);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}