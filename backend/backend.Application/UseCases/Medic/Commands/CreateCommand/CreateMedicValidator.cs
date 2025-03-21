// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.UseCases.User.Commands.CreateCommand;
using FluentValidation;

namespace backend.Application.UseCases.Medic.Commands.CreateCommand;

internal class CreateMedicValidator : AbstractValidator<CreateMedicCommand>
{
    public CreateMedicValidator()
    {
        RuleFor(x => x.Specialty)
            .NotNull().WithMessage("La Especialidad no puede ser nulo.")
            .NotEmpty().WithMessage("La Especialidad no puede ser vacio.");
        RuleFor(x => x.Phone)
            .NotNull().WithMessage("El Telefono no puede ser nulo.")
            .NotEmpty().WithMessage("El Telefono no puede ser vacio.");
    }
}