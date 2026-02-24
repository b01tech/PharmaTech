namespace PharmaTech.Product.Category.Dtos.Responses;

public record CategoryResponse(string Name, string Alias)
{
    public static CategoryResponse FromModel(Models.Category category) =>
        new(category.Name.Value, category.Alias.Value);
}
