using System;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        Result<Order> Update(Order order, DateTime orderDate, Guid customerId, decimal totalAmount);
    }
}
