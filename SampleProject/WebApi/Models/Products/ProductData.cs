using BusinessEntities;
using System.Collections.Generic;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            if (product != null)
            {
                Name = product.Name;
                Price = product.Price;
                Quantity = product.Quantity;
            }
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}