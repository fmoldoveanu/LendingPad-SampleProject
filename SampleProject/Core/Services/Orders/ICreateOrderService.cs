using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        BusinessEntities.Order Create(Guid id, DateTime dateTime, Guid customerId, decimal totalAmount);
    }
}