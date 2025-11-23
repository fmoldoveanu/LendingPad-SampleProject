using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateProductService : IUpdateProductService
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Result<Product> Update(Product product, string name, decimal price, int quantity)
        {
            var errors = new List<string>();

            try { product.SetName(name); }
            catch (Exception ex) { errors.Add(ex.Message); }

            try { product.SetPrice(price); }
            catch (Exception ex) { errors.Add(ex.Message); }

            try { product.SetQuantity(quantity); }
            catch (Exception ex) { errors.Add(ex.Message); }

            if (errors.Any())
                return Result<Product>.Fail(errors.ToArray());

            // Persist changes
            _productRepository.Save(product);

            return Result<Product>.Ok(product);
        }
    }
}
