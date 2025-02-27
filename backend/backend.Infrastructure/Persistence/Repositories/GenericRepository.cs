using backend.Application.Interfaces.Persistence;
using backend.Domain.Entities;
using backend.Infrastructure.Persistence.Context;
using Dapper;
using System.Data;
using System.Data.Entity;

namespace backend.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public async Task<bool> ExecAsync(string storedProcedure, object parameters)
    {
        using var connection = _context.CreateConnection;
        var objParam = new DynamicParameters(parameters);
        var recordAffected = await connection
            .ExecuteAsync(storedProcedure, param: objParam, commandType: CommandType.StoredProcedure);
        return recordAffected > 0;
    }

    public async Task<IEnumerable<T>> GetAllWithPaginationAsync(string storedProcedure, object parameter)
    {
        using var connection = _context.CreateConnection;
        var objParam = new DynamicParameters(parameter);
        return await connection
            .QueryAsync<T>(storedProcedure, param: objParam, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CountAsync(string tableName)
    {
        using var connection = _context.CreateConnection;
        var query = $"SELECT COUNT(1) FROM {tableName}";

        var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
        return count;
    }

    public IQueryable<T> GetAllQueryable()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetSelectAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}