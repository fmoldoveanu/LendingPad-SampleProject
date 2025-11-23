using System;
using BusinessEntities;
using Common.Results;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        Result<Order> DeleteById(Guid id);
        Result<Order> Delete(Order order);
        void DeleteAll();
    }
}
