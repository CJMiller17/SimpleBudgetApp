using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BudgetApp.Controllers
{
    public class DashboardController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        private int GetSelectedUserId()
        {
            // Get selected user ID from session, default to 1 if not set
            return _httpContextAccessor.HttpContext.Session.GetInt32("SelectedUserId") ?? 1;
        }

        public async Task<ActionResult> Index()
        {
            
            int userId = GetSelectedUserId();
    
            // Get user info for the sidebar
            var user = await _context.Users.FindAsync(userId);
            ViewBag.UserName = user?.Name ?? "Default User";
            ViewBag.UserProfileImage = user?.ProfileImagePath ?? "profile.jpg";
            
            // Find the most recent transaction by its date
            DateTime? latestTransactionDate = await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .Select(t => t.Date)
                .FirstOrDefaultAsync();
    
           // If no transactions exist, use today's date
            DateTime endDate = latestTransactionDate ?? DateTime.Today;
            DateTime startDate = endDate.AddDays(0); //-6
    
            // Will get the last 7 days of transactions
            List<Transaction> selectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= startDate && y.Date <= endDate && y.UserId == userId)
                .ToListAsync();

            
            /* Widget Calculations */
            
            // Total Income
            decimal totalIncome = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);
            ViewBag.TotalIncome = totalIncome.ToString("C2");

            // Total Expense
            decimal totalExpense = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);
            ViewBag.TotalExpense = totalExpense.ToString("C2");

            // Net Balance
            decimal balance = totalIncome - totalExpense;
            CultureInfo usCulture = CultureInfo.CreateSpecificCulture("en-US");
            usCulture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.Balance = String.Format(usCulture, "{0:C2}", balance);

            
            /* Doughnut Chart */
            
            // Expense By Category
            ViewBag.DoughnutChartData = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C2"),
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            
            /* Spline Chart */

            // Income
            List<SplineChartData> incomeSummary = selectedTransactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("MMM d"),
                    income = k.Sum(l => l.Amount)
                })
                .ToList();

            // Expense
            List<SplineChartData> expenseSummary = selectedTransactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("MMM d"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            // Total Amount
            string[] last7Days = Enumerable.Range(0, 7)
                .Select(i => startDate.AddDays(i).ToString("MMM d"))
                .ToArray();

            ViewBag.SplineChartData = from day in last7Days
                
                                      join income in incomeSummary on day equals income.day into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      
                                      join expense in expenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };
            
           
            /* Recent Transactions Widget */
            
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();


            return View();
        }
    }
    
    public class SplineChartData
    {
        public string day;
        public decimal income;
        public decimal expense;

    }
}