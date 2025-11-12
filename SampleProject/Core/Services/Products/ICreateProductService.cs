using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        BusinessEntities.Product Create(Guid id, string name, decimal price, int quantity);
    }
}