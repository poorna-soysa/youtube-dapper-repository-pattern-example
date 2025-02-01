namespace Products.Api.Dtos;

public sealed record CreateProductRequest(string Name, decimal Price, int Stock);

public sealed record UpdateProductRequest(Guid Id, string Name, decimal Price, int Stock);