// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Client.Commands.CreateCommand;

public class CreateClientValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientValidator()
    {
        RuleFor(x => x.Quantity)
            .NotNull().WithMessage("El ID de Cantidad no puede ser nulo.")
            .NotEmpty().WithMessage("El ID de Cantidad no puede ser vacio.");
        RuleFor(x => x.Productid)
            .NotNull().WithMessage("El ID de Producto no puede ser nulo.")
            .NotEmpty().WithMessage("El ID de Producto no puede ser vacio.");
    }
}