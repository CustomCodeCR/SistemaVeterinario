// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;

namespace backend.Application.Commons.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<BaseError>? Errors { get; }

    public ValidationException() : base()
    {
        Errors = new List<BaseError>();
    }

    public ValidationException(IEnumerable<BaseError> errors) : this()
    {
        Errors = errors;
    }
}