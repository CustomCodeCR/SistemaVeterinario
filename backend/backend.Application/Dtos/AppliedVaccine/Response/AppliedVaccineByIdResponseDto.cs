﻿// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.AppliedVaccine.Response;

public class AppliedVaccineByIdResponseDto
{
    public int AppliedVaccineId { get; set; }
    public DateTime Applicationdate { get; set; }
    public int PetId { get; set; }
    public int VaccineId { get; set; }
}