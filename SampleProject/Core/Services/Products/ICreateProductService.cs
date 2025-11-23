using System;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Result<Product> Create(string name, decimal price, int quantity);
    }
}
