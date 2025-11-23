using System;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Result<Order> Create(DateTime orderDate, Guid customerId, decimal totalAmount);
    }
}
