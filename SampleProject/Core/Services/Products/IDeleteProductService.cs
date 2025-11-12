using BusinessEntities;

namespace Core.Services.Products
{
    public interface IDeleteProductService
    {
        void Delete(BusinessEntities.Product product);
        void DeleteAll();
    }
}