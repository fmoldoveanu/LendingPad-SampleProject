using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
//using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        /*
        private readonly IDocumentSession _documentSession;

        public UserRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<User> Get(UserTypes? userType = null, string name = null, string email = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<User, UsersListIndex>();

            var hasFirstParameter = false;
            if (userType != null)
            {
                query = query.WhereEquals("Type", (int)userType);
                hasFirstParameter = true;
            }

            if (name != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                else
                {
                    hasFirstParameter = true;
                }
                query = query.Where($"Name:*{name}*");
            }

            if (email != null)
            {
                if (hasFirstParameter)
                {
                    query = query.AndAlso();
                }
                query = query.WhereEquals("Email", email);
            }
            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll<UsersListIndex>();
        }
    }
    */

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
}