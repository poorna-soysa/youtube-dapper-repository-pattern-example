﻿namespace Products.Api.Entities;

public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0.0m;
    public int Stock { get; set; } = 0;
}
