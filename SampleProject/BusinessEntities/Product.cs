using System;
using System.Collections.Generic;
using Common.Extensions;

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
            private set => _name = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public string SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Name was not provided.";
            }
            _name = name;
            return "";
        }

        public string SetPrice(decimal price)
        {
            if (price < 0)
            {
                return "Price cannot be negative.";
            }
            _price = price;
            return "";
        }

        public string SetQuantity(int quantity)
        {
            if (quantity < 0)
            {
                return "Quantity cannot be negative.";
            }
            _quantity = quantity;
            return "";
        }
    }
}