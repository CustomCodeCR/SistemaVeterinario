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

namespace backend.Application.UseCases.ProductCategory.Queries.GetByIdQuery
{
    public class GetProductCategoryByIdQueryHandler 
        : IRequestHandler<GetProductCategoryByIdQuery, BaseResponse<ProductCategoryByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductCategoryByIdResponseDto>> Handle(
            GetProductCategoryByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponse<ProductCategoryByIdResponseDto>();

            try
            {
                var productCategory = await _unitOfWork.ProductCategory.GetByIdAsync(request.CategoryId);

                if (productCategory is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.IsSuccess = true;
                response.Data = _mapper.Map<ProductCategoryByIdResponseDto>(productCategory);
                response.Message = ReplyMessage.MESSAGE_QUERY;
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
}