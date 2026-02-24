namespace PharmaTech.Product.Product.Dtos.Responses;

public record ProductResponses(
    Guid Id,
    string Name,
    string Alias,
    string Description,
    string Sku,
    decimal Price,
    Guid SubcategoryId
);
