// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Supplier.Commands.CreateCommand;

public class CreateSupplierValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El nombre no puede ser vacio.");
        RuleFor(x => x.Contact)
            .NotNull().WithMessage("El contacto no puede ser nulo.")
            .NotEmpty().WithMessage("El contacto no puede ser vacio.");
    }
}