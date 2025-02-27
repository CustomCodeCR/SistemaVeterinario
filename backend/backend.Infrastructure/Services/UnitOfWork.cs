using backend.Application.Interfaces.Persistence;
using backend.Application.Interfaces.Services;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;

namespace backend.Infrastructure.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
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