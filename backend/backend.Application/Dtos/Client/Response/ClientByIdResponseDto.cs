// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

namespace backend.Application.Dtos.Client.Response;

public class ClientByIdResponseDto
{
    public int ClientId { get; set; }
    public int UserId { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public int State { get; set; }
}