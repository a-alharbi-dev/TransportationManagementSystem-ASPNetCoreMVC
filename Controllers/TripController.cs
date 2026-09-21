using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class TripController : Controller
    {
        private readonly TransportationDbContext _context;

        public TripController(TransportationDbContext context)
        {
            _context = context;
        }


        // ==========================================
        // TRIP MANAGEMENT PAGE
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var trips = await _context.Trips
                .OrderBy(t => t.TripID)
                .ToListAsync();

            return View(
                "~/Views/Manager/TripManagement.cshtml",
                trips
            );
        }


        // ==========================================
        // CREATE TRIP
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Create(
            string pickupLocation,
            string destination,
            TimeSpan departureTime,
            TimeSpan arrivalTime)
        {
            try
            {
                // Validate Pickup Location
                if (string.IsNullOrWhiteSpace(pickupLocation))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Pickup location is required."
                    });
                }


                // Validate Destination
                if (string.IsNullOrWhiteSpace(destination))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Destination is required."
                    });
                }


                // ==========================================
                // CREATE NEW TRIP
                // ==========================================

                var trip = new Trip
                {
                    PickupLocation = pickupLocation,
                    Destination = destination,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime
                };


                _context.Trips.Add(trip);

                await _context.SaveChangesAsync();


                // ==========================================
                // SUCCESS
                // ==========================================

                return Json(new
                {
                    success = true,
                    message = "Trip created successfully.",
                    tripId = trip.TripID
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to create trip.",
                    error = ex.Message
                });
            }
        }



        // ==========================================
        // UPDATE TRIP
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Update(
            int tripId,
            string pickupLocation,
            string destination,
            TimeSpan departureTime,
            TimeSpan arrivalTime)
        {
            try
            {
                var trip = await _context.Trips
                    .FirstOrDefaultAsync(t => t.TripID == tripId);

                if (trip == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Trip not found."
                    });
                }


                // Update the existing trip

                trip.PickupLocation = pickupLocation;

                trip.Destination = destination;

                trip.DepartureTime = departureTime;

                trip.ArrivalTime = arrivalTime;


                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Trip updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to update trip.",
                    error = ex.Message
                });
            }
        }


        // ==========================================
        // DELETE TRIP
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var trip = await _context.Trips
                    .FirstOrDefaultAsync(t => t.TripID == id);


                if (trip == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Trip not found."
                    });
                }


                _context.Trips.Remove(trip);

                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Trip deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete trip.",
                    error = ex.Message
                });
            }
        }
    }
}