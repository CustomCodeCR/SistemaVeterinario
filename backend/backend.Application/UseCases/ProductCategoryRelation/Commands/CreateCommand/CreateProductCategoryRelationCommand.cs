// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.ProductCategoryRelation.Commands.CreateCommand;

public class CreateProductCategoryRelationCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}