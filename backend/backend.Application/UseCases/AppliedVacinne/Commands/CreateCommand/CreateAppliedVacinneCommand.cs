// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.AppliedVaccine.Commands.CreateCommand;

public class CreateAppliedVaccineCommand : IRequest<BaseResponse<bool>>
{
    public string AppliedVaccineId { get; set; } = null!;
    public DateTime Applicationdate { get; set; }
    public int Petid { get; set; }
    public int Vaccineid { get; set; }
    public int AuditCreateUser { get; set; }
}