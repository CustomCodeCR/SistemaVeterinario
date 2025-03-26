// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;
using backend.Application.UseCases.PurchaseOrder.Commands.CreateCommand;

public class CreatePurchaseOrderValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderValidator()
    {
        RuleFor(x => x.OrderDate)
            .NotEmpty().WithMessage("La fecha de la orden no puede estar vacía.")
            .GreaterThan(DateTime.MinValue).WithMessage("La fecha de la orden debe ser válida.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("El estado de la orden no puede estar vacío.");

        RuleFor(x => x.PurchaseOrderDetail)
            .NotEmpty().WithMessage("Debe incluir al menos un detalle en la orden de compra.");

        RuleForEach(x => x.PurchaseOrderDetail).SetValidator(new CreatePurchaseOrderDetailValidator());
    }
}

public class CreatePurchaseOrderDetailValidator : AbstractValidator<CreatePurchaseOrderDetailCommand>
{
    public CreatePurchaseOrderDetailValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Debe especificar el producto.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a cero.");
    }
}
