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
    public class OrderRepository : Repository<Order>, IOrderRepository
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
        public IEnumerable<Order> Get(DateTime? orderDate = null, Guid? customerId = null, decimal totalAmount = 0)
        {
            var query = Store.Values.AsQueryable();


            if (orderDate!= null)
                query = query.Where(u => u.OrderDate != null && u.OrderDate == orderDate);


            if (customerId != null)
                query = query.Where(u => u.CustomerId != null && u.CustomerId == customerId);

            if (totalAmount > 0)
                query = query.Where(u => u.TotalAmount > 0 && u.TotalAmount == totalAmount);

            return query.ToList();
        }

        public void DeleteAll()
        {
            base.DeleteAll();
        }
    }
}