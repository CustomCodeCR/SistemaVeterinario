// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.ProductCategory.Commands.CreateCommand;

public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryCommand>
{
    public CreateProductCategoryValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotNull().WithMessage("El nombre no puede ser nulo.")
            .NotEmpty().WithMessage("El nombre no puede ser vacio.");
        RuleFor(x => x.Description)
            .NotNull().WithMessage("La descripcion no puede ser nulo.")
            .NotEmpty().WithMessage("La descripcion no puede ser vacio.");
    }
}