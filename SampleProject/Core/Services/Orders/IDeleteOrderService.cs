using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        void Delete(BusinessEntities.Order order);
        void DeleteAll();
    }
}