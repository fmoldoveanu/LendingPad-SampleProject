using BusinessEntities;
using Common;
using Core.Services.Products;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private IOrderRepository _orderRepository;

        public DeleteOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Delete(BusinessEntities.Order order)
        {
            _orderRepository.Delete(order);
        }

        public void DeleteAll()
        {
            _orderRepository.DeleteAll();
        }
    }
}