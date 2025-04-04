// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.ProductCategory.Commands.DeleteCommand;

public class DeleteProductCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int ProductCategoryId { get; set; }
    public int AuditDeleteUser { get; set; }
}