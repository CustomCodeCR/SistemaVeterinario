using backend.Domain.Entities;

namespace backend.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAllQueryable();
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetSelectAsync();
    Task<T> GetByIdAsync(int id);
    Task<bool> ExecAsync(string storedProcedure, object parameters);
}