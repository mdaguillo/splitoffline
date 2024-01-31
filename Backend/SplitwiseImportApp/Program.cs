using BudgetAPI.EntityFramework.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;

namespace SplitwiseImportApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Set up configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.json") // Adjust the filename as needed
                .Build();

            // Set up DbContext
            var optionsBuilder = new DbContextOptionsBuilder<BudgetAppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BudgetAppDbContextString"));
            var filesToImport = configuration.GetSection("SplitwiseImport:ImportFiles").Get<List<ImportFile>>();

            foreach (var file in filesToImport )
            {
                using (var context = new BudgetAppDbContext(optionsBuilder.Options))
                {
                    using (var reader = new StreamReader(file.path))
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
                                DateCreated = DateTime.ParseExact(row.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                IsPaid = row.IsPaid
                            };

                            // Retrieve or create Category
                            var category = context.Categories.FirstOrDefault(c => c.CategoryName == file.category);
                            if (category == null)
                            {
                                category = new Category
                                {
                                    Id = Guid.NewGuid(),
                                    CategoryName = file.category
                                };

                                context.Categories.Add(category);
                            }

                            expense.CategoryID = category.Id;

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

                                context.Users.Add(catherine);
                            }

                            if (mike == null)
                            {
                                mike = new User
                                {
                                    Id = Guid.NewGuid(),
                                    Username = "mdaguillo",
                                    Email = "mdaguillo@gmail.com"
                                };

                                context.Users.Add(mike);
                            }

                            var catherineParticipant = new ExpenseParticipant
                            {
                                ParticipantId = catherine.Id,
                                AmountOwed = row.CatherineAmount
                            };

                            var mikeParticipant = new ExpenseParticipant
                            {
                                ParticipantId = mike.Id,
                                AmountOwed = row.MikeAmount
                            };

                            expense.ExpenseParticipants.Add(catherineParticipant);
                            expense.ExpenseParticipants.Add(mikeParticipant);

                            context.Expenses.Add(expense);

                            // Save changes to the database
                            context.SaveChanges();
                        }
                    }
                }
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
        public bool IsPaid { get; set; }
    }

    public class ImportFile
    {
        public string category { get; set; }
        public string path { get; set; }
    }
}