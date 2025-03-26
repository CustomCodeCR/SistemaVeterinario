// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Commons.Bases;

public class BaseFilters : BasePagination
{
    public int? NumFilter { get; set; }
    public string? TextFilter { get; set; }
    public int? StateFilter { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public bool? Download { get; set; } = false;
}