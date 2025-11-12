using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        string Update(BusinessEntities.Order order, DateTime orderDate, Guid customerId, decimal totalAmount);
    }
}