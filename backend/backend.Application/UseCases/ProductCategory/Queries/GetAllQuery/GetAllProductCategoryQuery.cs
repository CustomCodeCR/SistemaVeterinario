using backend.Application.Commons.Bases;
using backend.Application.Dtos.ProductCategory.Response;
using MediatR;

namespace backend.Application.UseCases.ProductCategory.Queries.GetAllQuery;

public class GetAllProductCategoryQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<ProductCategoryResponseDto>>>
{
}