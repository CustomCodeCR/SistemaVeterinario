// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using FluentValidation;

namespace backend.Application.UseCases.ProductCategoryRelation.Commands.CreateCommand;

public class CreateProductCategoryRelationValidator : AbstractValidator<CreateProductCategoryRelationCommand>
{
    public CreateProductCategoryRelationValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("El ID del producto debe ser mayor que cero.");
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("El ID de la categoría debe ser mayor que cero.");
    }
}