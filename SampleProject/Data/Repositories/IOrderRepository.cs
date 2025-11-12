using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> Get(DateTime? orderDate = null, Guid? customerId = null, decimal totalAmount = 0);
        void DeleteAll();
    }
}