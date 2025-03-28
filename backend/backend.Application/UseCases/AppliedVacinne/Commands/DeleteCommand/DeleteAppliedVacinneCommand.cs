// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.AppliedVaccine.Commands.DeleteCommand;

public class DeleteAppliedVaccineCommand : IRequest<BaseResponse<bool>>
{
    public int AppliedVaccineId { get; set; }
    public int AuditDeleteUser { get; set; }
}