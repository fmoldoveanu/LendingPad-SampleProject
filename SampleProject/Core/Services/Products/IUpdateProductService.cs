using BusinessEntities;
using Common.Results;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        Result<Product> Update(Product product, string name, decimal price, int quantity);
    }
}
