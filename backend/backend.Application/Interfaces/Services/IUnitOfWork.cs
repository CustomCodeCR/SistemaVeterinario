// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using System.Data;

namespace backend.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Appuser> User { get; }
    IGenericRepository<Client> Client { get; }
    IGenericRepository<Medic> Medic { get; }
    IGenericRepository<Pet> Pet { get; }
    IGenericRepository<Vaccine> Vaccine { get; }
    IGenericRepository<Appliedvaccine> AppliedVaccine { get; }
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}