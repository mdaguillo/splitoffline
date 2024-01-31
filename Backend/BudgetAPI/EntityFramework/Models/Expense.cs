namespace BudgetAPI.EntityFramework.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class Expense
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; } = 0;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; }
        public Guid? CategoryID { get; set; }
        public Category? Category { get; set; }
        public List<ExpenseParticipant> ExpenseParticipants { get; set; } = new List<ExpenseParticipant>();
    }

    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasIndex(e => e.IsPaid);
            builder.HasIndex(e => e.Description);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.Amount).HasColumnType("decimal(9,2)").IsRequired();
            builder.Property(e => e.DateCreated).IsRequired();

            // Configure the relationship to Category (optional)
            builder.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure the relationship to ExpenseParticipants
            builder.HasMany(e => e.ExpenseParticipants)
                .WithOne(ep => ep.Expense);
        }
    }
}
