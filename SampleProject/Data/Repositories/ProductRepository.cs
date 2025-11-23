using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        private readonly IDocumentSession _documentSession;

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Product> Get(string name = null, decimal? price = null, int? quantity = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductListIndex>();
            var hasFirstParameter = false;

            if (name != null)
            {
                query = query.WhereEquals("Name", name); // strict match
                hasFirstParameter = true;
            }

            if (price != null)
            {
                if (hasFirstParameter)
                    query = query.AndAlso();

                query = query.WhereEquals("Price", price.ToString()); // cast if stored as string
                hasFirstParameter = true;
            }

            if (quantity != null)
            {
                if (hasFirstParameter)
                    query = query.AndAlso();

                query = query.WhereEquals("Quantity", quantity.ToString()); // cast if stored as string
            }

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<ProductListIndex>();
        }
    }
    /*

        public IEnumerable<Product> Get(string name = null, decimal price = 0, int quantity = 0)
        {
            var query = Store.Values.AsQueryable();


            if (!string.IsNullOrEmpty(name))
                query = query.Where(u => u.Name != null && u.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);


            if (price > 0)
                query = query.Where(u => u.Price > 0 && u.Price == price);

            if (quantity > 0)
                query = query.Where(u => u.Quantity > 0 && u.Quantity == quantity);

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll();
        }
    }
    */
}