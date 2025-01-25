using Dapper;
using Microsoft.Data.SqlClient;
using Products.Api.Database;
using Products.Api.Dtos;
using Products.Api.Entities;

namespace Products.Api.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ProductDbContext context) =>
        {
            const string query = "SELECT Id,Name,Price,Stock FROM Product";

            using var connection = context.Create();

            var products = await connection.QueryAsync<Product>(query);

            return Results.Ok(products);
        });

        app.MapGet("products/{id:guid}", async (Guid id, ProductDbContext context) =>
        {
            const string query = "SELECT Id,Name,Price,Stock FROM Product WHERE Id=@Id";

            using var connection = context.Create();

            var product = await connection.QuerySingleOrDefaultAsync<Product>(
                query,
                new { Id = id });

            return product is not null ? Results.Ok(product) : Results.NotFound();
        })
        .WithName("GetProductById");

        app.MapPost("products", async (CreateProductRequest request, ProductDbContext context) =>
        {
            const string sql = "INSERT INTO Product(Id,Name,Price,Stock)" +
            " VALUES(@Id,@Name,@Price,@Stock)";

            using var connection = context.Create();

            await connection.ExecuteAsync(
                sql,
                new
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock
                });

            return Results.Created();
        });

        app.MapPut("products/{id:guid}", async (
            Guid id,
            UpdateProductRequest request,
            ProductDbContext context) =>
        {
            const string sql = "UPDATE Product" +
                               " SET Name=@Name," +
                               "     Price=@Price," +
                               "     Stock=@Stock" +
                               " WHERE Id=@Id";

            using var connection = context.Create();

            await connection.ExecuteAsync(sql, request);

            return Results.NoContent();
        });

        app.MapDelete("products/{id:guid}", async (
            Guid id,
            ProductDbContext context) =>
        {
            const string sql = "DELETE Product WHERE Id=@Id";

            using var connection = context.Create();

            await connection.ExecuteAsync(sql, new {  Id = id});

            return Results.NoContent();
        });
    }
}
