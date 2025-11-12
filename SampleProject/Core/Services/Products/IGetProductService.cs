using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        BusinessEntities.Product GetProduct(Guid id);

        IEnumerable<BusinessEntities.Product> GetProducts(string name = null, decimal price = 0, int quantity = 0);
    }
}