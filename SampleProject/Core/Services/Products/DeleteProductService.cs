using System;
using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Result<Product> DeleteById(Guid id)
        {
            var product = _productRepository.Get(id);
            if (product == null)
                return Result<Product>.Fail("Product not found.");

            _productRepository.Delete(product);
            return Result<Product>.Ok(product);
        }

        public Result<Product> Delete(Product product)
        {
            if (product == null)
                return Result<Product>.Fail("Product cannot be null.");

            _productRepository.Delete(product);
            return Result<Product>.Ok(product);
        }

        public void DeleteAll()
        {
            _productRepository.DeleteAll();
        }
    }
}
