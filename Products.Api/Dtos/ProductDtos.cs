namespace Products.Api.Dtos;

public sealed record CreateProductRequest(string Name, decimal Price, int Stock);