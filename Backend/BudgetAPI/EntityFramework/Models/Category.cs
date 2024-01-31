using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BudgetAPI.EntityFramework.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.CategoryName).IsUnique();

            builder.HasKey(c => c.Id);
            builder.Property(c => c.CategoryName).IsRequired();
            builder.HasMany(c => c.Expenses).WithOne(c => c.Category);
        }
    }
}
