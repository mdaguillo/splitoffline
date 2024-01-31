using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.EntityFramework.Models
{
    public class RecurringExpense
    {
        public Guid Id { get; set; }

        /// <summary>
        ///  The description must be manually synced to an expense
        /// </summary>
        public string Description { get; set; } = string.Empty;
        public int IntervalDays { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }

    public class RecurringExpenseConfiguration : IEntityTypeConfiguration<RecurringExpense>
    {
        public void Configure(EntityTypeBuilder<RecurringExpense> builder)
        {
            builder.HasIndex(x => x.Description).IsUnique();

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.DateCreated).IsRequired();
        }
    }
}
