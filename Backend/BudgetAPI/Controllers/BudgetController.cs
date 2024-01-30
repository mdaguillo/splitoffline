using BudgetAPI.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly BudgetAppDbContext _dbContext;
        public BudgetController(ILogger<BudgetController> logger, BudgetAppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<UserViewModel> Get()
        {
            _logger.LogInformation("Received request for expenses");
            
            var users = _dbContext.Users
                        .Include(x => x.Expenses)
                        .ThenInclude(e => e.Expense)
                        .Select(x => x.ToViewModel())
                        .ToList();

            return users; 
        }
    }
}
