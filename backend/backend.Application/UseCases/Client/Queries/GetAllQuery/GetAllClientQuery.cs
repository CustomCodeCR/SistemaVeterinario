// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.Client.Response;
using MediatR;

namespace backend.Application.UseCases.Client.Queries.GetAllQuery;

public class GetAllClientQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<ClientResponseDto>>>
{
}