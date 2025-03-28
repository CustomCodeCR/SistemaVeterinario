// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Vaccine.Commands.UpdateCommand;

public class UpdateVaccineCommand : IRequest<BaseResponse<bool>>
{
    public int VaccineId { get; set; }
    public string VaccineName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}