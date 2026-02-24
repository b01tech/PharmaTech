namespace PharmaTech.Product.Product.Dtos.Requests;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Alias,
    string Description,
    string Sku,
    decimal Price,
    Guid SubcategoryId
);
