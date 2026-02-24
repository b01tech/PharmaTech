using PharmaTech.Core.Base;
using PharmaTech.Product.Product.Repositories;

namespace PharmaTech.Product.Product.UseCases;

public class DeleteProductUseCase(
    IProductReadOnlyRepository readOnlyRepository,
    IProductWriteRepository writeRepository)
    : IDeleteProductUseCase
{
    public async Task<Result> ExecuteAsync(Guid id)
    {
        var product = await readOnlyRepository.GetProductByIdAsync(id);
        if (product is null)
            return Result.Failure("PRODUCT_NOT_FOUND");

        await writeRepository.DeleteProductAsync(product);

        return Result.Success();
    }
}
