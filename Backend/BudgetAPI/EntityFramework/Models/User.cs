using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BudgetAPI.Controllers.ViewModels;

namespace BudgetAPI.EntityFramework.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<ExpenseParticipant> Expenses { get; set; }

        public UserViewModel ToViewModel()
        {
            return new UserViewModel
            {
                Username = Username,
                Email = Email,
                CurrentExpenses = Expenses.Select(x => new UserExpenseViewModel
                {
                    ExpenseId = x.ExpenseID,
                    ExpenseDescription = x.Expense.Description,
                    AmountOwed = x.AmountOwed
                }).ToList()
            };
        }
    }

    public class UserViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserExpenseViewModel> CurrentExpenses { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.Email).IsRequired();

            builder.HasMany(u => u.Expenses)
                .WithOne(ep => ep.User);
        }
    }
}
