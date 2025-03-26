// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Product.Commands.DeleteCommand;

public class DeleteProductCommand : IRequest<BaseResponse<bool>>
{
    public int ProductId { get; set; }
    public int AuditDeleteUser { get; set; }
}