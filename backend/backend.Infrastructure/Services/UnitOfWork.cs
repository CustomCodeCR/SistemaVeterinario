// -----------------------------------------------------------------------------
// Copyright (c) 2024 CustomCodeCR. All rights reserved.
// Developed by: Maurice Lang Bonilla
// -----------------------------------------------------------------------------

using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using backend.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace backend.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private bool _disposed = false;

    private IGenericRepository<Appuser> _user = null!;
    private IGenericRepository<Client> _client = null!;
    private IGenericRepository<Medic> _medic = null!;
    private IGenericRepository<Pet> _pet = null!;
    private IGenericRepository<Vaccine> _vaccine = null!;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<Appuser> User => _user ?? new GenericRepository<Appuser>(_context);
    public IGenericRepository<Client> Client => _client ?? new GenericRepository<Client>(_context);
    public IGenericRepository<Medic> Medic => _medic ?? new GenericRepository<Medic>(_context);
    public IGenericRepository<Pet> Pet => _pet ?? new GenericRepository<Pet>(_context);
    public IGenericRepository<Vaccine> Vaccine => _vaccine ?? new GenericRepository<Vaccine>(_context);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    public IDbTransaction BeginTransaction()
    {
        var transaction = _context.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }
}