// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.ProductCategory.Commands.UpdateCommand;

public class UpdateProductCategoryCommand : IRequest<BaseResponse<bool>>
{
    public int ProductCategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}