// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Commons.Select.Response;
using MediatR;

namespace backend.Application.UseCases.Inventory.Queries.GetSelectQuery;

public class GetSelectInventoryQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}