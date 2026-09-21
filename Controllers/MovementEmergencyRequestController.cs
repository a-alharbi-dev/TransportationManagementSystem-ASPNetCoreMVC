using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class MovementEmergencyRequestController : Controller
    {
        private readonly TransportationDbContext _context;

        public MovementEmergencyRequestController(
            TransportationDbContext context)
        {
            _context = context;
        }


        // ==========================================
        // SHOW EMERGENCY REQUESTS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Make sure Movement Officer is logged in
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // Get all emergency requests
            // submitted by Nurses
            var requests = await _context.EmergencyRequests
                .Include(e => e.User)
                .Include(e => e.Driver)
                .Include(e => e.Vehicle)
                .OrderByDescending(e => e.RequestID)
                .ToListAsync();


            return View(requests);
        }


        // ==========================================
        // APPROVE REQUEST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(
            int id,
            int driverId,
            int vehicleId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            // Find emergency request
            var request = await _context.EmergencyRequests
                .FirstOrDefaultAsync(e => e.RequestID == id);


            if (request == null)
            {
                return NotFound();
            }


            // Assign driver
            request.DriverID = driverId;


            // Assign vehicle
            request.VehicleID = vehicleId;


            // Change status
            request.Status = "Approved";


            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] =
                "Emergency request approved successfully.";


            return RedirectToAction("Index");
        }


        // ==========================================
        // REJECT REQUEST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }


            var request = await _context.EmergencyRequests
                .FirstOrDefaultAsync(e => e.RequestID == id);


            if (request == null)
            {
                return NotFound();
            }


            request.Status = "Rejected";


            await _context.SaveChangesAsync();


            TempData["SuccessMessage"] =
                "Emergency request rejected.";


            return RedirectToAction("Index");
        }
    }
}