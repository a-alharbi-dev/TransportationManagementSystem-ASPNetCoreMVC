using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class EmergencyRequestsController : Controller
    {
        private readonly TransportationDbContext _context;

        public EmergencyRequestsController(TransportationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // SHOW EMERGENCY REQUESTS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Check if user is logged in
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // Get emergency requests
            var requests = await _context.EmergencyRequests
                .Include(e => e.User)
                .Include(e => e.Driver)
                .Include(e => e.Vehicle)
                .OrderByDescending(e => e.RequestID)
                .ToListAsync();


            // Get drivers for approve modal
            ViewBag.Drivers = await _context.Drivers
                .ToListAsync();


            // Get vehicles for approve modal
            ViewBag.Vehicles = await _context.Vehicles
                .ToListAsync();


            return View(requests);
        }


        // =========================================================
        // APPROVE EMERGENCY REQUEST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(
            int requestId,
            int driverId,
            int vehicleId)
        {
            // Check if Movement Officer is logged in
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // =====================================================
            // FIND EMERGENCY REQUEST
            // =====================================================

            var request = await _context.EmergencyRequests
                .FirstOrDefaultAsync(e => e.RequestID == requestId);


            if (request == null)
            {
                TempData["ErrorMessage"] =
                    "Emergency request was not found.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // ONLY PENDING REQUESTS CAN BE APPROVED
            // =====================================================

            if (request.Status != "Pending")
            {
                TempData["ErrorMessage"] =
                    "This emergency request has already been processed.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // CHECK DRIVER
            // =====================================================

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(d => d.DriverID == driverId);


            if (driver == null)
            {
                TempData["ErrorMessage"] =
                    "Selected driver was not found.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // CHECK VEHICLE
            // =====================================================

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(v => v.VehicleID == vehicleId);


            if (vehicle == null)
            {
                TempData["ErrorMessage"] =
                    "Selected vehicle was not found.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // ASSIGN DRIVER
            // =====================================================

            request.DriverID = driverId;


            // =====================================================
            // ASSIGN VEHICLE
            // =====================================================

            request.VehicleID = vehicleId;


            // =====================================================
            // CHANGE STATUS
            // =====================================================

            request.Status = "Approved";


            // =====================================================
            // CREATE NOTIFICATION FOR NURSE
            // =====================================================

            var notification = new Notification
            {
                UserID = request.UserID,

                Message =
                    $"Your emergency request has been approved. " +
                    $"Driver: {driver.FullName}. " +
                    $"Vehicle: {vehicle.PlateNumber}.",

                CreatedAt = DateTime.Now,

                Status = "New"
            };


            _context.Notifications.Add(notification);


            // =====================================================
            // SAVE EVERYTHING
            // =====================================================

            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] =
                "Emergency request approved successfully.";


            return RedirectToAction("Index");
        }


        // =========================================================
        // REJECT EMERGENCY REQUEST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int requestId)
        {
            // Check if Movement Officer is logged in
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // =====================================================
            // FIND REQUEST
            // =====================================================

            var request = await _context.EmergencyRequests
                .FirstOrDefaultAsync(e => e.RequestID == requestId);


            if (request == null)
            {
                TempData["ErrorMessage"] =
                    "Emergency request was not found.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // ONLY PENDING REQUESTS CAN BE REJECTED
            // =====================================================

            if (request.Status != "Pending")
            {
                TempData["ErrorMessage"] =
                    "This emergency request has already been processed.";

                return RedirectToAction("Index");
            }


            // =====================================================
            // CHANGE STATUS
            // =====================================================

            request.Status = "Rejected";


            // =====================================================
            // CREATE NOTIFICATION FOR NURSE
            // =====================================================

            var notification = new Notification
            {
                UserID = request.UserID,

                Message =
                    "Your emergency request has been rejected.",

                CreatedAt = DateTime.Now,

                Status = "New"
            };


            _context.Notifications.Add(notification);


            // =====================================================
            // SAVE EVERYTHING
            // =====================================================

            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] =
                "Emergency request rejected.";


            return RedirectToAction("Index");
        }
    }
}