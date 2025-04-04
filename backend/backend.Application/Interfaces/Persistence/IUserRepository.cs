// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<Appuser>
{
    Task<Appuser> UserByUsername(string username);
}