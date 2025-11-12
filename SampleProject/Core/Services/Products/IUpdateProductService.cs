using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        string Update(BusinessEntities.Product product, string name, decimal price, int quantity);
    }
}