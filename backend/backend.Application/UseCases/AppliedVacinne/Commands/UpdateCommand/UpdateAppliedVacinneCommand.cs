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
    public DateTime Applicationdate { get; set; }
    public int PetId { get; set; }
    public int VaccineId { get; set; }
    public int AuditUpdateUser { get; set; }
}