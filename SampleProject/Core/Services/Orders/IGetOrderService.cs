using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        BusinessEntities.Order GetOrder(Guid id);

        IEnumerable<BusinessEntities.Order> GetOrders(DateTime? orderDate = null, Guid? customerId = null, decimal totalAmount = 0);
    }
}