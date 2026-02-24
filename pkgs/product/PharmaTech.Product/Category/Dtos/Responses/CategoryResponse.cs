namespace PharmaTech.Product.Category.Dtos.Responses;

public record CategoryResponse(Guid Id, string Name, string Alias)
{
    public static CategoryResponse FromModel(Models.Category category) =>
        new(category.Id, category.Name.Value, category.Alias.Value);
}
