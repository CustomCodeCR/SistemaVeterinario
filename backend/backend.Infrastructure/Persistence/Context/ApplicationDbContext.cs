using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace backend.Infrastructure.Persistence.Context;

public class ApplicationDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("")!;
    }

    public IDbConnection CreateConnection => new OracleConnection(_connectionString);
}