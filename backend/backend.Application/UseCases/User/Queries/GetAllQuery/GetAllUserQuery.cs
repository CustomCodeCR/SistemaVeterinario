// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using backend.Application.Dtos.User.Response;
using MediatR;

namespace backend.Application.UseCases.User.Queries.GetAllQuery;

public class GetAllUserQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<UserResponseDto>>>
{
}