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
        RuleFor(x => x.Address)
            .NotNull().WithMessage("La Direccion no puede ser nulo.")
            .NotEmpty().WithMessage("La Direccion no puede ser vacio.");
        RuleFor(x => x.Phone)
            .NotNull().WithMessage("El Telefono no puede ser nulo.")
            .NotEmpty().WithMessage("El Telefono no puede ser vacio.");
    }
}