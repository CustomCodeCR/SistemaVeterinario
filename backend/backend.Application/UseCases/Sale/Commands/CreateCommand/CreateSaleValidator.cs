// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;
using backend.Application.UseCases.Sale.Commands.CreateCommand;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(x => x.SaleDate)
            .NotEmpty().WithMessage("La fecha de la venta no puede estar vacía.")
            .GreaterThan(DateTime.MinValue).WithMessage("La fecha de la venta debe ser válida.");

        RuleFor(x => x.SaleDetail)
            .NotEmpty().WithMessage("Debe incluir al menos un detalle en la orden de venta.");

        RuleForEach(x => x.SaleDetail).SetValidator(new CreateSaleDetailValidator());
    }
}

public class CreateSaleDetailValidator : AbstractValidator<CreateSaleDetailCommand>
{
    public CreateSaleDetailValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Debe especificar el producto.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor a cero.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");
    }
}
