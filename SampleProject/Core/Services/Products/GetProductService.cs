using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;

        public GetProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public BusinessEntities.Product GetProduct(Guid id)
        {
            return _productRepository.Get(id);
        }

        public IEnumerable<BusinessEntities.Product> GetProducts(string name = null, decimal price = 0, int quantity=0)
        {
            return _productRepository.Get(name, price, quantity);
        }
    }
}