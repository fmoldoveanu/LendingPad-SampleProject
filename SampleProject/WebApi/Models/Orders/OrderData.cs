using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            if (order != null)
            {
                OrderDate = order.OrderDate;
                CustomerId = order.CustomerId;
                TotalAmount = order.TotalAmount;
            }
        }

        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}