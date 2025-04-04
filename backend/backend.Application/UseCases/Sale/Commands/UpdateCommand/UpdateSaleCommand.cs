// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Sale.Commands.UpdateCommand;

public class UpdateSaleCommand : IRequest<BaseResponse<bool>>
{
    public int SaleId { get; set; }
    public int ClientId { get; set; }
    public DateTime SaleDate { get; set; }
    public int State { get; set; }
    public int AuditUpdateUser {  get; set; }
    public IEnumerable<UpdateSaleDetailCommand> SaleDetail { get; set; } = null!;
}

public class UpdateSaleDetailCommand
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
}