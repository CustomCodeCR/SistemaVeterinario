// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;
using backend.Application.UseCases.Appointment.Commands.CreateCommand;

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.AppointmentDate)
            .NotEmpty().WithMessage("La fecha de la cita no puede ser vacía.")
            .GreaterThan(DateTime.MinValue).WithMessage("La fecha de la cita debe ser válida.");

        RuleFor(x => x.Reason)
            .NotNull().WithMessage("La razón no puede ser nula.")
            .NotEmpty().WithMessage("La razón no puede estar vacía.");

        RuleFor(x => x.PetId)
            .GreaterThan(0).WithMessage("Debe seleccionar una mascota válida.");

        RuleFor(x => x.MedicId)
            .GreaterThan(0).WithMessage("Debe seleccionar un médico válido.");

        RuleFor(x => x.AppointmentDetail)
            .NotEmpty().WithMessage("Debe incluir al menos un detalle para la cita.");

        RuleForEach(x => x.AppointmentDetail).SetValidator(new CreateAppointmentDetailValidator());
    }
}

public class CreateAppointmentDetailValidator : AbstractValidator<CreateAppointmentDetailCommand>
{
    public CreateAppointmentDetailValidator()
    {
        RuleFor(x => x.Diagnosis)
            .NotNull().WithMessage("El diagnóstico no puede ser nulo.")
            .NotEmpty().WithMessage("El diagnóstico no puede estar vacío.");

        RuleFor(x => x.Treatment)
            .NotNull().WithMessage("El tratamiento no puede ser nulo.")
            .NotEmpty().WithMessage("El tratamiento no puede estar vacío.");

        RuleFor(x => x.Observations)
            .NotNull().WithMessage("Las observaciones no pueden ser nulas.")
            .NotEmpty().WithMessage("Las observaciones no pueden estar vacías.");
    }
}