// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Vaccine.Commands.CreateCommand;

public class CreateVaccineValidator : AbstractValidator<CreateVaccineCommand>
{
    public CreateVaccineValidator()
    {
        RuleFor(x => x.VaccineName)
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El nombre no puede ser vacio.");
        RuleFor(x => x.Type)
            .NotNull().WithMessage("El tipo no puede ser nulo.")
            .NotEmpty().WithMessage("El tipo no puede ser vacio.");
    }
}