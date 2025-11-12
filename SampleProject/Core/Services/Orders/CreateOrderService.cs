using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IIdObjectFactory<BusinessEntities.Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<BusinessEntities.Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
        }

        public BusinessEntities.Order Create(Guid id, DateTime orderDate, Guid customerId, decimal totalAmount)
        {
            var existing = _orderRepository.Get(id);
            if (existing != null)
                return null;

            var order = _orderFactory.Create(id);
            _updateOrderService.Update(order, orderDate, customerId, totalAmount);
            _orderRepository.Save(order);
            return order;
        }
    }
}