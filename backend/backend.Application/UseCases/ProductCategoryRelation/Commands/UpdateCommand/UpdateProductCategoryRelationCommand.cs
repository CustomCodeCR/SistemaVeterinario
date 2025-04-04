// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.ProductCategoryRelation.Commands.UpdateCommand;

public class UpdateProductCategoryRelationCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
    public int CategoryId { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}