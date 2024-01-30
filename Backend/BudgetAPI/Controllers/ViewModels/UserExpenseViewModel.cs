namespace BudgetAPI.Controllers.ViewModels
{
    public class UserExpenseViewModel
    {
        public Guid ExpenseId { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal AmountOwed { get; set; }
    }
}
