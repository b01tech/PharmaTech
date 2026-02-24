namespace PharmaTech.Product.Product.Dtos.Requests;

public record CreateProductRequest(
    string Name,
    string Alias,
    string Description,
    string Sku,
    decimal Price,
    Guid SubcategoryId
);
