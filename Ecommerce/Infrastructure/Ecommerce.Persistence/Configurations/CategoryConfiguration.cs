using Ecommerce.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            Category category1 = new()
            {
                Id = 1,
                Name = "Electronic",
                Priority = 1,
                ParentId = 0,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            Category category2 = new()
            {
                Id = 2,
                Name = "Fashion",
                Priority = 2,
                ParentId = 0,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            Category parent1 = new()
            {
                Id = 3,
                Name = "Computer",
                Priority = 1,
                ParentId = 1,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            Category parent2 = new()
            {
                Id = 4,
                Name = "Woman",
                Priority = 1,
                ParentId = 2,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };

            builder.HasData(category1, category2, parent1, parent2);
        }
    }
}