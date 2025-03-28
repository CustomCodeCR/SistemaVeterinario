// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Vaccine.Commands.DeleteCommand;

public class DeleteVaccineCommand : IRequest<BaseResponse<bool>>
{
    public int VaccineId { get; set; }
    public int AuditDeleteUser { get; set; }
}