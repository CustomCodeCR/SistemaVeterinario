using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using backend.Infrastructure.Persistence.Repositories;
using System.Transactions;

namespace backend.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<User> User { get; }

    public UnitOfWork(ApplicationDbContext context, IGenericRepository<User> user)
    {
        User = user;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}