using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Common.Results;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class UpdateOrderService : IUpdateOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Result<Order> Update(Order order, DateTime orderDate, Guid customerId, decimal totalAmount)
        {
            var errors = new List<string>();

            try { order.SetOrderDate(orderDate); }
            catch (Exception ex) { errors.Add(ex.Message); }

            try { order.SetCustomerId(customerId); }
            catch (Exception ex) { errors.Add(ex.Message); }

            try { order.SetTotalAmount(totalAmount); }
            catch (Exception ex) { errors.Add(ex.Message); }

            if (errors.Any())
                return Result<Order>.Fail(errors.ToArray());

            // Persist changes
            _orderRepository.Save(order);

            return Result<Order>.Ok(order);
        }
    }
}
