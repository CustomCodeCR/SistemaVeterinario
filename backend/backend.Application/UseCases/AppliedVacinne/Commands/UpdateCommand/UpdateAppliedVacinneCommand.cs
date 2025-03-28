// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.AppliedVaccine.Commands.UpdateCommand;

public class UpdateAppliedVaccineCommand : IRequest<BaseResponse<bool>>
{
    public int AppliedVaccineId { get; set; }
    public string AppliedVaccineName { get; set; } = null!;
    public DateTime Applicationdate { get; set; }
    public int Petid { get; set; }
    public int Vaccineid { get; set; }
    public int AuditCreateUser { get; set; }
}