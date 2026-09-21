using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;

namespace TransportationManagementSystem.Controllers
{
    public class NotificationController : Controller
    {
        private readonly TransportationDbContext _context;

        public NotificationController(TransportationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // SHOW USER NOTIFICATIONS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // =====================================================
            // CHECK LOGIN
            // =====================================================

            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // =====================================================
            // GET ONLY CURRENT USER NOTIFICATIONS
            // =====================================================

            var notifications = await _context.Notifications
                .Where(n => n.UserID == userId.Value)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();


            return View(notifications);
        }


        // =========================================================
        // MARK NOTIFICATION AS READ
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            // =====================================================
            // CHECK LOGIN
            // =====================================================

            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return Unauthorized();
            }


            // =====================================================
            // FIND NOTIFICATION
            // ONLY FOR CURRENT USER
            // =====================================================

            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>
                    n.NotificationID == id &&
                    n.UserID == userId.Value);


            // =====================================================
            // NOTIFICATION NOT FOUND
            // =====================================================

            if (notification == null)
            {
                return NotFound();
            }


            // =====================================================
            // CHANGE STATUS TO READ
            // =====================================================

            if (notification.Status == "New")
            {
                notification.Status = "Read";

                await _context.SaveChangesAsync();
            }


            // =====================================================
            // RETURN SUCCESS
            // =====================================================

            return Json(new
            {
                success = true,
                id = notification.NotificationID,
                status = notification.Status
            });
        }
    }
}