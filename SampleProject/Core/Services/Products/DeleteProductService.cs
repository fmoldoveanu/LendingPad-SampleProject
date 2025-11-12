using BusinessEntities;
using Common;
using Core.Services.Products;
using Data.Repositories;

namespace Core.Services.Product
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private IProductRepository _productRepository;

        public DeleteProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Delete(BusinessEntities.Product product)
        {
            _productRepository.Delete(product);
        }

        public void DeleteAll()
        {
            _productRepository.DeleteAll();
        }
    }
}