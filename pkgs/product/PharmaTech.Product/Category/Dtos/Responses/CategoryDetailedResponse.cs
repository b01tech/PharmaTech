namespace PharmaTech.Product.Category.Dtos.Responses;

public record CategoryDetailedResponse(
    Guid Id,
    string Name,
    string Alias,
    DateTime CreatedAt,
    IEnumerable<SubCategoryDetailedResponse> Subcategories);

public record SubCategoryDetailedResponse(Guid Id, string Name, string Alias);
