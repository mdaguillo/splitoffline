using BudgetAPI.EntityFramework.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace SplitwiseImportApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // Adjust the filename as needed
                .Build();

            // Set up DbContext
            var optionsBuilder = new DbContextOptionsBuilder<BudgetAppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BudgetAppDbContextString"));

            using (var context = new BudgetAppDbContext(optionsBuilder.Options))
            {
                // Define CSV file path
                var csvFilePath = "C:\\Users\\Mike\\source\\repos\\BudgetApp\\vacation-travel_2024-01-30_export.csv";

                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                }))
                {
                    var records = csv.GetRecords<ExpenseCsvModel>();
                    foreach (var row in records)
                    {
                        // Create new Expense object
                        var expense = new Expense
                        {
                            Id = Guid.NewGuid(),
                            Description = row.Description,
                            Amount = row.ExpenseCost,
                            DateCreated = DateTime.ParseExact(row.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture)
                        };

                        // Retrieve or create Category
                        var category = context.Categories.FirstOrDefault(c => c.CategoryName == row.CategoryName)
                                       ?? new Category
                                       {
                                           Id = Guid.NewGuid(),
                                           CategoryName = row.CategoryName
                                       };

                        expense.Category = category;

                        // Add ExpenseParticipants (Catherine and Mike)
                        var catherine = context.Users.FirstOrDefault(u => u.Username == "cyochum");
                        var mike = context.Users.FirstOrDefault(u => u.Username == "mdaguillo");
                        if (catherine == null)
                        {
                            catherine = new User
                            {
                                Id = Guid.NewGuid(),
                                Email = "cyochum@alumni.cmu.edu",
                                Username = "cyochum"
                            };
                        }

                        if (mike == null)
                        {
                            mike = new User
                            {
                                Id = Guid.NewGuid(),
                                Username = "mdaguillo",
                                Email = "mdaguillo@gmail.com"
                            };
                        }

                        var catherineParticipant = new ExpenseParticipant
                        {
                            ParticipantId = catherine.Id,
                            Share = 0,
                            AmountOwed = row.CatherineAmount
                        };

                        var mikeParticipant = new ExpenseParticipant
                        {
                            ParticipantId = mike.Id,
                            Share = 0,
                            AmountOwed = row.MikeAmount
                        };

                        expense.ExpenseParticipants.Add(catherineParticipant);
                        expense.ExpenseParticipants.Add(mikeParticipant);

                        context.Expenses.Add(expense);
                    }
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }

    public class ExpenseCsvModel
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public decimal ExpenseCost { get; set; }
        public decimal CatherineAmount { get; set; }
        public decimal MikeAmount { get; set; }
    }
}