using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Common.Entities
{
    public class Detail : EntityBase
    {
        public Detail() { }
        public Detail(string title, string description, int categoryId)
        {
            Title = title;
            Description = description;
            CategoryId = categoryId;
        }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}