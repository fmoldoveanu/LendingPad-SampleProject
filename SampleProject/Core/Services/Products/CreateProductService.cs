using System;
using BusinessEntities;
using Common;
using Common.Results;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class CreateProductService : ICreateProductService
    {
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductRepository _productRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory, IProductRepository productRepository)
        {
            _productFactory = productFactory;
            _productRepository = productRepository;
        }

        public Result<Product> Create(string name, decimal price, int quantity)
        {
            var id = Guid.NewGuid();
            if (_productRepository.Get(id) != null)
                return Result<Product>.Fail($"Product with ID {id} already exists.");

            var product = _productFactory.Create(id);

            try
            {
                product.SetName(name);
                product.SetPrice(price);
                product.SetQuantity(quantity);
            }
            catch (Exception ex)
            {
                return Result<Product>.Fail(ex.Message);
            }

            _productRepository.Save(product);
            return Result<Product>.Ok(product);
        }
    }
}
