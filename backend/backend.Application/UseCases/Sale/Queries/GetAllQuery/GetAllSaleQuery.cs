// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Sale.Response;
using MediatR;

namespace backend.Application.UseCases.Sale.Queries.GetAllQuery;

public class GetAllSaleQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<SaleResponseDto>>>
{
}