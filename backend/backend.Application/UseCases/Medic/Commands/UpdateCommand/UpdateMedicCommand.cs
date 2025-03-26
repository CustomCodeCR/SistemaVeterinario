// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Medic.Commands.UpdateCommand;

public class UpdateMedicCommand : IRequest<BaseResponse<bool>>
{
    public int MedicId { get; set; }
    public int UserId { get; set; }
    public string Specialty { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int State { get; set; }
    public int AuditUpdateUser { get; set; }
}