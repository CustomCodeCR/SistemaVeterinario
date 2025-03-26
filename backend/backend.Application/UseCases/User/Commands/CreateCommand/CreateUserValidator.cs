// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull().WithMessage("El Nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El Nombre no puede ser vacio.");
        RuleFor(x => x.LastName)
            .NotNull().WithMessage("El Apellido no puede ser nulo.")
            .NotEmpty().WithMessage("El Apellido no puede ser vacio.");
        RuleFor(x => x.Username)
            .NotNull().WithMessage("El Usuario no puede ser nulo.")
            .NotEmpty().WithMessage("El Usuario no puede ser vacio.");
        RuleFor(x => x.Password)
            .NotNull().WithMessage("La Contraseña no puede ser nulo.")
            .NotEmpty().WithMessage("La Contraseña no puede ser vacio.");
        RuleFor(x => x.Email)
            .NotNull().WithMessage("El Correo no puede ser nulo.")
            .NotEmpty().WithMessage("El Correo no puede ser vacio.");
    }
}