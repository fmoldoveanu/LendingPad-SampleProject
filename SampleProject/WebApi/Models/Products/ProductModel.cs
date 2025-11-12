using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}