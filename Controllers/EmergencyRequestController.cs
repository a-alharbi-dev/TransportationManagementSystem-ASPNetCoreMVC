using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class EmergencyRequestController : Controller
    {
        private readonly TransportationDbContext _context;

        public EmergencyRequestController(TransportationDbContext context)
        {
            _context = context;
        }


        // ==========================================
        // CREATE - SHOW EMERGENCY REQUEST FORM
        // ==========================================

        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }


        // ==========================================
        // CREATE - SAVE EMERGENCY REQUEST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            string PickupLocation,
            string Destination,
            TimeSpan PickupTime,
            string Reason,
            string? Notes)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // ==========================================
            // CHECK USER
            // ==========================================

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.UserID == userId.Value &&
                    u.IsActive);

            if (user == null)
            {
                HttpContext.Session.Clear();

                return RedirectToAction("Index", "Login");
            }


            // ==========================================
            // VALIDATE FORM
            // ==========================================

            if (string.IsNullOrWhiteSpace(PickupLocation))
            {
                ViewBag.Error = "Please select a pickup location.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(Destination))
            {
                ViewBag.Error = "Please select a destination.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(Reason))
            {
                ViewBag.Error = "Please select a reason.";
                return View();
            }


            // ==========================================
            // CREATE REQUEST TIME
            // ==========================================

            var today = DateTime.Today;

            var requestTime = today.Add(PickupTime);


            // ==========================================
            // CREATE EMERGENCY REQUEST
            // ==========================================

            var emergencyRequest = new EmergencyRequest
            {
                UserID = userId.Value,

                PickupLocation = PickupLocation,

                Destination = Destination,

                Priority = "High",

                RequestTime = requestTime,

                Reason = Reason,

                Notes = Notes,

                Status = "Pending"
            };


            // ==========================================
            // SAVE
            // ==========================================

            _context.EmergencyRequests.Add(emergencyRequest);

            await _context.SaveChangesAsync();


            // ==========================================
            // SUCCESS MESSAGE
            // ==========================================

            TempData["SuccessMessage"] =
                "Emergency request submitted successfully.";


            return RedirectToAction("Create");
        }
    }
}