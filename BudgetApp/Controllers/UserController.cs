using BudgetApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

        public async Task<IActionResult> UserSelection()
        {
            SetUserInfoInViewBag();
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult SelectUser(int id)
        {
            // Store selected user ID in session
            _httpContextAccessor.HttpContext.Session.SetInt32("SelectedUserId", id);
            return RedirectToAction("Index", "Dashboard");
        }
    }
}