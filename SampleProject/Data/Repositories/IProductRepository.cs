using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string name = null, decimal price = 0, int quantity = 0);
        void DeleteAll();
    }
}