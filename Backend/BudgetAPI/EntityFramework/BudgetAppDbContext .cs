using BudgetAPI.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

public class BudgetAppDbContext : DbContext
{
    public BudgetAppDbContext(DbContextOptions<BudgetAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseParticipant> ExpenseParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply the entity configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseParticipantConfiguration());
    }
}