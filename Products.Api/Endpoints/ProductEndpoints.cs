using Dapper;
using Microsoft.Data.SqlClient;
using Products.Api.Dtos;
using Products.Api.Entities;

namespace Products.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (IConfiguration configuration) =>
        {
            const string query = "SELECT * FROM Product";

            string connectionString = configuration.GetConnectionString("DbConnectionString");

            using SqlConnection connection = new(connectionString);

            var products = await connection.QueryAsync<Product>(query);

            Results.Ok(products);
        });

        app.MapPost("products", async (CreateProductRequest request,IConfiguration configuration) =>
        {
            const string sql = "INSERT INTO Product(Id,Name,Price,Stock)" +
                               " VALUES (@Id,@Name,@Price,@Stock)";

            string connectionString = configuration.GetConnectionString("DbConnectionString");

            using SqlConnection connection = new(connectionString);

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price,
                    Stock  = request.Stock
                });

            Results.Created();
        });
    }
}
