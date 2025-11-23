using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Data.Indexes;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        
        private readonly IDocumentSession _documentSession;

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public IEnumerable<Order> Get(DateTime? orderDate = null, Guid? customerId = null, decimal? totalAmount = null)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order, OrderListIndex>();
            var hasFirstParameter = false;

            if (orderDate != null)
            {
                var start = orderDate.Value.Date;
                var end = start.AddDays(1);

                query = query.WhereBetween("OrderDate", start, end);
                hasFirstParameter = true;
            }

            if (customerId != null)
            {
                if (hasFirstParameter)
                    query = query.AndAlso();

                query = query.WhereEquals("CustomerId", customerId.Value.ToString());
                hasFirstParameter = true;
            }

            if (totalAmount != null)
            {
                if (hasFirstParameter)
                    query = query.AndAlso();

                query = query.Where($"TotalAmount:{totalAmount.Value}*");
            }

            return query.ToList();
        }


        public void DeleteAll()
        {
            base.DeleteAll<OrderListIndex>();
        }
    }
   /*
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
    */
}