// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Commons.Bases;
using MediatR;

namespace backend.Application.UseCases.Medic.Commands.DeleteCommand;

public class DeleteMedicCommand : IRequest<BaseResponse<bool>>
{
    public int MedicId { get; set; }
    public int AuditDeleteUser { get; set; }
}