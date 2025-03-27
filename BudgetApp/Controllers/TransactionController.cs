using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;
using Microsoft.AspNetCore.Http;

namespace BudgetApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Helper method to get the selected user ID from session
        private int GetSelectedUserId()
        {
            // Get selected user ID from session, default to 1 if not set
            return _httpContextAccessor.HttpContext.Session.GetInt32("SelectedUserId") ?? 1;
        }

        // GET: Get the list of transactions for the selected user
        public async Task<IActionResult> Index()
        {
            int userId = GetSelectedUserId();
            
            // Get user info for the view
            var user = await _context.Users.FindAsync(userId);
            ViewBag.UserName = user?.Name ?? "Default User";
            ViewBag.UserProfileImage = user?.ProfileImagePath ?? "profile.jpg";
            
            // Filter transactions by user ID
            var transactions = await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.UserId == userId)
                .ToListAsync();
                
            return View(transactions);
        }

        // GET: Get the Transaction AddOrEdit view
        public IActionResult AddOrEdit(int id = 0)
        {
            int userId = GetSelectedUserId();
            
            // Get user info for the view
            var user = _context.Users.Find(userId);
            ViewBag.UserName = user?.Name ?? "Default User";
            ViewBag.UserProfileImage = user?.ProfileImagePath ?? "profile.jpg";
            
            PopulateCategories();
            
            if (id == 0)
            {
                // For new transactions, create with the current user ID
                return View(new Transaction { UserId = userId });
            }
            else
            {
                // For existing transactions, verify it belongs to the current user
                var transaction = _context.Transactions.Find(id);
                if (transaction != null && transaction.UserId != userId)
                {
                    // If the transaction belongs to another user, redirect to index
                    return RedirectToAction(nameof(Index));
                }
                return View(transaction);
            }
        }

        // POST: Create or edit a Transaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date,UserId")] Transaction transaction)
        {
            int userId = GetSelectedUserId();
            
            // Ensure the transaction is associated with the current user
            transaction.UserId = userId;
            
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                    _context.Add(transaction);
                else
                {
                    // Find the existing entity and update its properties
                    var existingTransaction = await _context.Transactions.FindAsync(transaction.TransactionId);
                    if (existingTransaction != null)
                    {
                        if (existingTransaction.UserId != userId)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                
                        // Update the properties of the existing entity
                        existingTransaction.CategoryId = transaction.CategoryId;
                        existingTransaction.Amount = transaction.Amount;
                        existingTransaction.Note = transaction.Note;
                        existingTransaction.Date = transaction.Date;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transaction);
        }

        // POST: Deletes a transaction using its ID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetSelectedUserId();
            
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions' is null.");
            }
            
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                // Only delete if the transaction belongs to the current user
                if (transaction.UserId == userId)
                {
                    _context.Transactions.Remove(transaction);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var categoryCollection = _context.Categories.ToList();
            Category defaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            categoryCollection.Insert(0, defaultCategory);
            ViewBag.Categories = categoryCollection;
        }
    }
}
