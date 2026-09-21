using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly TransportationDbContext _context;

        public LoginController(TransportationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Username == model.Username &&
                    u.Password == model.Password &&
                    u.IsActive);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View(model);
            }

            // ==========================================
            // SAVE LOGGED-IN USER INFORMATION
            // ==========================================

            HttpContext.Session.SetInt32("UserID", user.UserID);

            HttpContext.Session.SetString("Username", user.Username);

            HttpContext.Session.SetString("Role", user.Role);


            // ==========================================
            // REDIRECT BASED ON ROLE
            // ==========================================

            if (user.Role == "Nurse")
            {
                return RedirectToAction("Create", "EmergencyRequest");
            }

            if (user.Role == "Manager")
            {
                return RedirectToAction("Index", "Trip");
            }

            if (user.Role == "Movement Officer")
            {
                return RedirectToAction("Index", "Assignment");
            }

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Error = "User role is not recognized.";

            return View(model);
        }
    }
}