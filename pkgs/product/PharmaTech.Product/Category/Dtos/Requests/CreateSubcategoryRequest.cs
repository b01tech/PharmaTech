namespace PharmaTech.Product.Category.Dtos.Requests;

public record CreateSubcategoryRequest(string Name, string Alias, Guid CategoryId);
