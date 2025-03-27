using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;

namespace BudgetApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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
        
        // Helper method to set user info in ViewBag
        private void SetUserInfoInViewBag()
        {
            int userId = GetSelectedUserId();
            
            // Get user info for the view
            var user = _context.Users.Find(userId);
            ViewBag.UserName = user?.Name ?? "Default User";
            ViewBag.UserProfileImage = user?.ProfileImagePath ?? "profile.jpg";
        }
        
        // GET: Get the list of categories
        public async Task<IActionResult> Index()
        {
            SetUserInfoInViewBag();
            return View(await _context.Categories.ToListAsync());
        }
        
        // GET: Get the AddOrEdit view
        public IActionResult AddOrEdit(int id=0)
        {
            SetUserInfoInViewBag();
            if (id == 0)
                return View(new Category());
            else
                return View(_context.Categories.Find(id));

        }

        // POST: Creates or updates a category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            SetUserInfoInViewBag();
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                    _context.Add(category);
                else
                    _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        
        // POST: Delete a category using its ID
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
