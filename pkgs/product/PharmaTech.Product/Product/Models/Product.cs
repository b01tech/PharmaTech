using PharmaTech.Core.Base;
using PharmaTech.Core.ValueObjects;

namespace PharmaTech.Product.Product.Models;

public class Product : Entity
{
    public Name Name { get; private set; }
    public Alias Alias { get; private set; }
    public Description Description { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public Sku Sku { get; private set; }
    public decimal Price { get; private set; }
    public Guid SubcategoryId { get; private set; }

    // EF
    protected Product() { }

    private Product(Name name, Alias alias, Description description, Sku sku, decimal price, Guid subcategoryId)
    {
        Name = name;
        Alias = alias;
        Description = description;
        Sku = sku;
        Price = price;
        SubcategoryId = subcategoryId;
    }

    public static Result<Product> Create(
        string nameInput,
        string aliasInput,
        string descriptionInput,
        string skuInput,
        decimal price,
        Guid subcategoryId
    )
    {
        var name = Core.ValueObjects.Name.Create(nameInput);
        var alias = Core.ValueObjects.Alias.Create(aliasInput);
        var description = Core.ValueObjects.Description.Create(descriptionInput);
        var sku = Core.ValueObjects.Sku.Create(skuInput);
        if (name.IsFailure || alias.IsFailure || description.IsFailure || sku.IsFailure)
            return Result<Product>.Failure(Result.MergeErrors(name, alias, description, sku));

        return new Product(name.Data, alias.Data, description.Data, sku.Data, price, subcategoryId);
    }

    public Result Update(
        string nameInput,
        string aliasInput,
        string descriptionInput,
        string skuInput,
        decimal price,
        Guid subcategoryId
    )
    {
        var name = Core.ValueObjects.Name.Create(nameInput);
        var alias = Core.ValueObjects.Alias.Create(aliasInput);
        var description = Core.ValueObjects.Description.Create(descriptionInput);
        var sku = Core.ValueObjects.Sku.Create(skuInput);
        if (name.IsFailure || alias.IsFailure || description.IsFailure || sku.IsFailure)
            return Result.Failure(Result.MergeErrors(name, alias, description, sku));

        Name = name.Data;
        Alias = alias.Data;
        Description = description.Data;
        Sku = sku.Data;
        Price = price;
        SubcategoryId = subcategoryId;

        return Result.Success();
    }
}
