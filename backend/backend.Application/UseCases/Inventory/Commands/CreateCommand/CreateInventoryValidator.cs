// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Inventory.Commands.CreateCommand;

public class CreateInventoryValidator : AbstractValidator<CreateInventoryCommand>
{
    public CreateInventoryValidator()
    {
        RuleFor(x => x.Quantity)
            .NotNull().WithMessage("La Cantidad no puede ser nulo.")
            .NotEmpty().WithMessage("La Cantidad no puede ser vacio.");
        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("El ID de Producto no puede ser nulo.")
            .NotEmpty().WithMessage("El ID de Producto no puede ser vacio.");
    }
}