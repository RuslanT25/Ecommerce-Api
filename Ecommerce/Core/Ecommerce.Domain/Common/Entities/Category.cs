﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Common.Entities
{
    public class Category : EntityBase
    {
        public Category()
        {

        }

        public Category(string name, int priority, int parentId)
        {
            Name = name;
            Priority = priority;
            ParentId = parentId;
        }

        public int ParentId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}