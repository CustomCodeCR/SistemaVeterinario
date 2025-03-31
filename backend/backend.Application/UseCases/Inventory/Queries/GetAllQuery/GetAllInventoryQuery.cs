// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Inventory.Response;
using MediatR;

namespace backend.Application.UseCases.Inventory.Queries.GetAllQuery;

public class GetAllInventoryQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<InventoryResponseDto>>>
{
}