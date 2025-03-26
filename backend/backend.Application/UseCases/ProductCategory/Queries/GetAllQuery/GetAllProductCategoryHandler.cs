// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Eduardo Castro
// -----------------------------------------------------------------------------
using AutoMapper;
using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategory.Response;
using backend.Application.Interfaces.Services;
using backend.Utilities.Static;
using MediatR;
using WatchDog;

namespace backend.Application.UseCases.ProductCategory.Queries.GetAllQuery;

public class GetAllProductCategoryHandler : IRequestHandler<GetAllProductCategoryQuery, BaseResponse<IEnumerable<ProductCategoryResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    //private readonly IOrderingQuery _ordering;

    public GetAllProductCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<ProductCategoryResponseDto>>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductCategoryResponseDto>>();

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