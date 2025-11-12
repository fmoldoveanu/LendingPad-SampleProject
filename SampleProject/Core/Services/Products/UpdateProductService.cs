using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Services.Products;

namespace Core.Services.Users
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public string Update(BusinessEntities.Product product, string name, decimal price, int quantity)
        {
            string ret = "";
            ret += product.SetName(name);
            ret += product.SetPrice(price);
            ret += product.SetQuantity(quantity);


            return ret;
        }
    }
}