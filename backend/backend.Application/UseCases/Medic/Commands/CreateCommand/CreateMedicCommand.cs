// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Medic.Commands.CreateCommand;

public class CreateMedicCommand : IRequest<BaseResponse<bool>>
{
    public int UserId { get; set; }
    public string Specialty { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int State { get; set; }
    public int AuditCreateUser { get; set; }
}