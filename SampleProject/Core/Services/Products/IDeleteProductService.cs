using System;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Products
{
    public interface IDeleteProductService
    {
        Result<Product> DeleteById(Guid id);
        Result<Product> Delete(Product product);
        void DeleteAll();
    }
}
