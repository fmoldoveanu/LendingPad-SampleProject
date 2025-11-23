using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> Get(string name = null, decimal? price = null, int? quantity = null);
        void DeleteAll();
    }
}