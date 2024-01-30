# Add necessary references to Entity Framework
# Add-Type -Path "C:\Users\Mike\source\repos\BudgetApp\Backend\BudgetAPI\bin\Debug\net6.0\Microsoft.EntityFrameworkCore.dll"
# Add-Type -Path "C:\Users\Mike\source\repos\BudgetApp\Backend\BudgetAPI\bin\Debug\net6.0\Microsoft.EntityFrameworkCore.SqlServer.dll"

Add-Type -AssemblyName "Microsoft.EntityFrameworkCore"
Add-Type -AssemblyName "Microsoft.EntityFrameworkCore.SqlServer"

# Set up Entity Framework DbContext
$contextType = [System.Type]::GetType("BudgetAppDbContext")
$context = [System.Activator]::CreateInstance($contextType)

# Define CSV file path
$csvFilePath = "C:\Users\Mike\Documents\subscriptions_2024-01-30_export.csv"

# Read CSV file
$data = Import-Csv -Path $csvFilePath

# Process each row in the CSV file
foreach ($row in $data) {
    # Convert date string to DateTime object
    $date = [DateTime]::ParseExact($row.Date, "yyyy-MM-dd", [CultureInfo]::InvariantCulture)

    # Create new Expense object
    $expense = New-Object BudgetAPI.EntityFramework.Models.Expense
    $expense.Description = $row.Description
    $expense.Amount = [decimal]$row.ExpenseCost
    $expense.DateCreated = $date

    # Retrieve or create Category
    $category = $context.Categories.FirstOrDefault({ $_.CategoryName -eq $row.CategoryName })
    if ($null -eq $category) {
        $category = New-Object BudgetAPI.EntityFramework.Models.Category
        $category.CategoryName = $row.CategoryName
        $context.Categories.Add($category)
    }
    $expense.Category = $category

    # Add ExpenseParticipants (Catherine and Mike)
    $catherine = $context.Users.FirstOrDefault({ $_.Username -eq "Catherine" })
    $mike = $context.Users.FirstOrDefault({ $_.Username -eq "Mike" })
    if ($null -eq $catherine) {
        $category = New-Object BudgetAPI.EntityFramework.Models.User
        $category.Username = "cyochum"
        $category.Email = "cyochum@alumni.cmu.edu"
        $context.Users.Add($category)
    }

    if ($null -eq $mike) {
        $category = New-Object BudgetAPI.EntityFramework.Models.User
        $category.Username = "mdaguillo"
        $category.Email = "mdaguillo@gmail.com"
        $context.Users.Add($category)
    }

    $catherineParticipant = New-Object BudgetAPI.EntityFramework.Models.ExpenseParticipant
    $catherineParticipant.ParticipantId = $catherine.Id
    $catherineParticipant.Share = [decimal]$row.CatherineAmount
    $catherineParticipant.AmountOwed = [decimal]$row.CatherineAmount
    $catherineParticipant.User = $catherine
    $catherineParticipant.Expense = $expense

    $mikeParticipant = New-Object BudgetAPI.EntityFramework.Models.ExpenseParticipant
    $mikeParticipant.ParticipantId = $mike.Id
    $mikeParticipant.Share = [decimal]$row.MikeAmount
    $mikeParticipant.AmountOwed = [decimal]$row.MikeAmount
    $mikeParticipant.User = $mike
    $mikeParticipant.Expense = $expense

    $context.Expenses.Add($expense)
}

# Save changes to the database
$context.SaveChanges()
