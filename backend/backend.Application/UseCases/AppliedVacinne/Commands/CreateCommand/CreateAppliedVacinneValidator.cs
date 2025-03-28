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
        RuleFor(x => x.AppliedVaccineName)
            .NotNull().WithMessage("El nombre de la vacuna no puede ser nulo.")
            .NotEmpty().WithMessage("El nombre de la vacuna no puede ser vacio.");
        RuleFor(x => x.Vaccineid)
            .NotNull().WithMessage("El ID de la vacuna no puede ser nulo.")
            .NotEmpty().WithMessage("El ID de la vacuna no puede ser vacio.");
    }
}