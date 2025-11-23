using System;

namespace BusinessEntities
{
    public class Product : IdObject
    {
        private string _name;
        private decimal _price;
        private int _quantity;

        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name must be provided.", nameof(Name));
                _name = value.Trim();
            }
        }

        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), "Price cannot be negative.");
                _price = value;
            }
        }

        public int Quantity
        {
            get => _quantity;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Quantity), "Quantity cannot be negative.");
                _quantity = value;
            }
        }

        // Public setters delegate to property validation
        public void SetName(string name) => Name = name;
        public void SetPrice(decimal price) => Price = price;
        public void SetQuantity(int quantity) => Quantity = quantity;
    }
}
