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

namespace backend.Application.UseCases.ProductCategoryRelation.Queries.GetAllQuery;

public class GetAllProductCategoryRelationHandler : IRequestHandler<GetAllProductCategoryRelationQuery, BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    //private readonly IOrderingQuery _ordering;

    public GetAllProductCategoryRelationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>> Handle(GetAllProductCategoryRelationQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductCategoryRelationResponseDto>>();

        try
        {
            
            
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}