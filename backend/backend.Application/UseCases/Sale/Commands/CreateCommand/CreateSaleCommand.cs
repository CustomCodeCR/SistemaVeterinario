// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Sale.Commands.CreateCommand;

public class CreateSaleCommand : IRequest<BaseResponse<bool>>
{
    public int ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
    public IEnumerable<CreateSaleDetailCommand> SaleDetail { get; set; } = null!;
}

public class CreateSaleDetailCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}