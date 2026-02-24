using PharmaTech.Core.Base;
using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Product.Category.Models;

public class Subcategory : Entity
{
    public Name Name { get; private set; }
    public Alias Alias { get; private set; }
    public Guid CategoryId { get; private set; }

    // EF
    protected Subcategory() { }

    private Subcategory(Name name, Alias alias, Guid categoryId)
    {
        Name = name;
        Alias = alias;
        CategoryId = categoryId;
    }

    public static Result<Subcategory> Create(string nameInput, string aliasInput, Guid categoryIdInput)
    {
        var name = Core.ValueObjects.Name.Create(nameInput);
        var alias = Core.ValueObjects.Alias.Create(aliasInput);
        if (name.IsFailure || alias.IsFailure)
            return Result<Subcategory>.Failure(Result.MergeErrors(name, alias));

        return new Subcategory(name.Data, alias.Data, categoryIdInput);
    }
}
