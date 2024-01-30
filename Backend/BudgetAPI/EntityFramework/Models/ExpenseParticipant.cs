namespace BudgetAPI.EntityFramework.Models
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ExpenseParticipant
    {
        public Guid ExpenseID { get; set; }
        public Guid ParticipantId { get; set; }
        public decimal Share { get; set; }
        public decimal AmountOwed { get; set; } 
        public User User { get; set; }
        public Expense Expense { get; set; }  
    }

    public class ExpenseParticipantConfiguration : IEntityTypeConfiguration<ExpenseParticipant>
    {
        public void Configure(EntityTypeBuilder<ExpenseParticipant> builder)
        {
            builder.HasKey(ep => new { ep.ExpenseID, ep.ParticipantId });
            builder.Property(ep => ep.ExpenseID);
            builder.Property(ep => ep.ParticipantId);
            builder.Property(ep => ep.AmountOwed).HasColumnType("decimal(9,2)").IsRequired();
            builder.Property(ep => ep.Share).HasColumnType("decimal(3,2)").IsRequired();

            builder.HasOne(ep => ep.Expense)
                .WithMany(e => e.ExpenseParticipants)
                .HasForeignKey(ep => ep.ExpenseID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ep => ep.User)
                .WithMany(u => u.Expenses)
                .HasForeignKey(ep => ep.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
