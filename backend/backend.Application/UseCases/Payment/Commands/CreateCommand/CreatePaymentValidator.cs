// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.Payment.Commands.CreateCommand;

public class CreatePaymentValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.Amount)
            .NotNull().WithMessage("El monto no puede ser nulo.")
            .NotEmpty().WithMessage("El monto no puede ser vacio.");
        RuleFor(x => x.PaymentType)
            .NotNull().WithMessage("El Tipo de pago no puede ser nulo.")
            .NotEmpty().WithMessage("El Tipo de pago no puede ser vacio.");
    }
}