using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<BusinessEntities.Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<BusinessEntities.Product> productFactory, IProductRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public BusinessEntities.Product Create(Guid id, string name, decimal price, int quantity)
        {
            var existing = _productRepository.Get(id);
            if (existing != null)
                return null;

            var product = _productFactory.Create(id);
            _updateProductService.Update(product, name, price, quantity);
            _productRepository.Save(product);
            return product;
        }
    }
}