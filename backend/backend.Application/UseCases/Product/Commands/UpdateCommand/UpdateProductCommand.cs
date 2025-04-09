// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace backend.Application.UseCases.Product.Commands.UpdateCommand;

public class UpdateProductCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public string Description { get; set; } = null!;
    public int Price { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}