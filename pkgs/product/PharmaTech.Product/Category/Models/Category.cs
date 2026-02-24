using PharmaTech.Core.Base;
using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Product.Category.Models;

public class Category : Entity
{
    public Name Name { get; private set; }
    public Alias Alias { get; private set; }
    public IList<Subcategory> Subcategories { get; private set; }

    // EF
    protected Category() { }

    private Category(Name name, Alias alias)
    {
        Name = name;
        Alias = alias;
        Subcategories = new List<Subcategory>();
    }

    public static Result<Category> Create(string nameInput, string aliasInput)
    {
        var name = Core.ValueObjects.Name.Create(nameInput);
        var alias = Core.ValueObjects.Alias.Create(aliasInput);

        if (name.IsFailure || alias.IsFailure)
            return Result<Category>.Failure(Result.MergeErrors(name, alias));
        return new Category(name.Data, alias.Data);
    }

    public void Add(Subcategory subcategory) => Subcategories.Add(subcategory);

    public void Remove(Subcategory subcategory) => Subcategories.Remove(subcategory);
}
