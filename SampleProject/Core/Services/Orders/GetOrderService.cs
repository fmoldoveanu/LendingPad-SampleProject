using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public BusinessEntities.Order GetOrder(Guid id)
        {
            return _orderRepository.Get(id);
        }

        public IEnumerable<BusinessEntities.Order> GetOrders(DateTime? orderDate = null, Guid? customerId = null, decimal totalAmount=0)
        {
            return _orderRepository.Get(orderDate, customerId, totalAmount);
        }
    }
}