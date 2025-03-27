using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetApp.Models;

namespace BudgetApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Get the list of categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }
        
        // GET: Get the AddOrEdit view
        public IActionResult AddOrEdit(int id=0)
        {
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
