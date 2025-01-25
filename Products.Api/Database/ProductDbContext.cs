using Microsoft.Data.SqlClient;
using System.Data;

namespace Products.Api.Database;

public sealed class ProductDbContext
{
    private readonly string _connectionString;

    public ProductDbContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DbConnectionString")!;
    }

    public IDbConnection Create() => new SqlConnection(_connectionString);
}
