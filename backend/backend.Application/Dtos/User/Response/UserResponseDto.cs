﻿namespace backend.Application.Dtos.User.Response;

public class UserResponseDto
{
    public int UsertId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserType { get; set; } = null!;
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateUser { get; set; }
}