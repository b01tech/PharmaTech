using PharmaTech.Product.Category.Models;

namespace PharmaTech.Product.Category.Dtos.Responses;

public record SubcategoryResponse(string Name, string Alias)
{
    public static SubcategoryResponse FromModel(Subcategory subcategory) =>
        new(subcategory.Name.Value, subcategory.Alias.Value);
}
