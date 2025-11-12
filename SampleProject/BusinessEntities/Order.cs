using System;
using System.Collections.Generic;
using Common.Extensions;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private DateTime _orderDate;
        private Guid _customerId;
        private decimal _totalAmount;


        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public Guid CustomerId
        {
            get => _customerId;
            private set => _customerId = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }

        public string SetOrderDate(DateTime orderDate)
        {
            if (orderDate == DateTime.MinValue)
            {
                return "Order date was not provided.";
            }
            _orderDate = orderDate;
            return "";
        }

        public string SetCustomerId(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return "Customer ID was not provided.";
            }
            _customerId = customerId;
            return "";
        }

        public string SetTotalAmount(decimal totalAmount)
        {
            if (totalAmount < 0)
            {
                return "Total amount cannot be negative.";
            }
            _totalAmount = totalAmount;
            return "";
        }
    }
}