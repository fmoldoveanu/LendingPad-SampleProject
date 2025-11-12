using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using Core.Services.Products;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public string Update(BusinessEntities.Order order, DateTime orderDate, Guid customerId, decimal totalAmount)
        {
            string ret = "";
            ret += order.SetOrderDate(orderDate);
            ret += order.SetCustomerId(customerId);
            ret += order.SetTotalAmount(totalAmount);


            return ret;
        }
    }
}