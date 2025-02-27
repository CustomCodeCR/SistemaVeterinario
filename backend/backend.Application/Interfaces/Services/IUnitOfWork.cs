using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using System.Data;

namespace backend.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> User {  get; }
    Task SaveChangesAsync();
    IDbTransaction BeginTransaction();
}