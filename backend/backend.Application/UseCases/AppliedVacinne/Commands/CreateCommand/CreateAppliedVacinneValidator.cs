// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.AppliedVaccine.Commands.CreateCommand;

public class CreateAppliedVaccineValidator : AbstractValidator<CreateAppliedVaccineCommand>
{
    public CreateAppliedVaccineValidator()
    {
        RuleFor(x => x.VaccineId)
            .NotNull().WithMessage("El ID de la vacuna no puede ser nulo.")
            .NotEmpty().WithMessage("El ID de la vacuna no puede ser vacio.");
    }
}