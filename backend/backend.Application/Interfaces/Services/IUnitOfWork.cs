using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;

namespace backend.Application.Interfaces.Services;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> User {  get; }
}