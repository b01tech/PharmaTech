namespace PharmaTech.Product.Product.Dtos.Requests;

public record ListProductsRequest(
    int Page,
    int PageSize,
    Guid? CategoryId,
    Guid? SubcategoryId
);
