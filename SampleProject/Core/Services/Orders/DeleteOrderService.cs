using System;
using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Result<Order> DeleteById(Guid id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
                return Result<Order>.Fail("Order not found.");

            _orderRepository.Delete(order);
            return Result<Order>.Ok(order);
        }

        public Result<Order> Delete(Order order)
        {
            if (order == null)
                return Result<Order>.Fail("Order cannot be null.");

            _orderRepository.Delete(order);
            return Result<Order>.Ok(order);
        }

        public void DeleteAll()
        {
            _orderRepository.DeleteAll();
        }
    }
}
