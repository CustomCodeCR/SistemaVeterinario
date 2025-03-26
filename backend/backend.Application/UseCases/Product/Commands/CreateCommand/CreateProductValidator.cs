// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Product.Commands.CreateCommand;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El nombre no puede ser vacio.");
        RuleFor(x => x.Description)
            .NotNull().WithMessage("La descripcion no puede ser nulo.")
            .NotEmpty().WithMessage("La descripcion no puede ser vacio.");
    }
}