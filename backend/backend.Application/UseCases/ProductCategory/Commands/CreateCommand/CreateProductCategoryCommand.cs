﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.ProductCategory.Commands.CreateCommand;

public class CreateProductCategoryCommand : IRequest<BaseResponse<bool>>
{
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}