using System;

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
            private set
            {
                if (value == DateTime.MinValue)
                    throw new ArgumentException("Order date must be provided.", nameof(OrderDate));
                _orderDate = value;
            }
        }

        public Guid CustomerId
        {
            get => _customerId;
            private set
            {
                if (value == Guid.Empty)
                    throw new ArgumentException("Customer ID must be provided.", nameof(CustomerId));
                _customerId = value;
            }
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalAmount), "Total amount cannot be negative.");
                _totalAmount = value;
            }
        }

        // Public setters delegate to property validation
        public void SetOrderDate(DateTime orderDate) => OrderDate = orderDate;
        public void SetCustomerId(Guid customerId) => CustomerId = customerId;
        public void SetTotalAmount(decimal totalAmount) => TotalAmount = totalAmount;
    }
}
