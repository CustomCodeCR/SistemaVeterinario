// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Pet.Commands.CreateCommand;

public class CreatePetValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.Type)
            .NotNull().WithMessage("El Tipo no puede ser nulo.")
            .NotEmpty().WithMessage("El Tipo no puede ser vacio.");
    }
}