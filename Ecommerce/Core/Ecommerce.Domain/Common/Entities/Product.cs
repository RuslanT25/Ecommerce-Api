using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Common.Entities
{
    public class Product : EntityBase
    {
        public Product() { }
        public Product(string title, string description, double price, double discount, int brandId)
        {
            Title = title;
            Description = description;
            Price = price;
            Discount = discount;
            BrandId = brandId;
        }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required double Price { get; set; }
        public required double Discount { get; set; }
        public required int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Category> Categories { get; set; }
        //public required string ImagePath { get; set; }
    }
}