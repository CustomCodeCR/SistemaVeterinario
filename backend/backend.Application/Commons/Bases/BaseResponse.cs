// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Commons.Bases;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public IEnumerable<BaseError>? Errors { get; set; }
    public int? TotalRecords { get; set; }
}