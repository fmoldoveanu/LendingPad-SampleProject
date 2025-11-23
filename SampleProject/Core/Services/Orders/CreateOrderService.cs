using System;
using BusinessEntities;
using Common;
using Common.Results;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Scope)]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
        }

        public Result<Order> Create(DateTime orderDate, Guid customerId, decimal totalAmount)
        {
            var id = Guid.NewGuid();
            if (_orderRepository.Get(id) != null)
                return Result<Order>.Fail($"Order with ID {id} already exists.");

            var order = _orderFactory.Create(id);

            try
            {
                order.SetOrderDate(orderDate);
                order.SetCustomerId(customerId);
                order.SetTotalAmount(totalAmount);
            }
            catch (Exception ex)
            {
                return Result<Order>.Fail(ex.Message);
            }

            _orderRepository.Save(order);
            return Result<Order>.Ok(order);
        }
    }
}
