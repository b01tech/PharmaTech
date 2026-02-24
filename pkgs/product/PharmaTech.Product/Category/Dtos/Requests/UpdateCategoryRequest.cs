namespace PharmaTech.Product.Category.Dtos.Requests;

public record UpdateCategoryRequest(Guid Id, string Name, string Alias);
