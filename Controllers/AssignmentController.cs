using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly TransportationDbContext _context;

        public AssignmentController(TransportationDbContext context)
        {
            _context = context;
        }


        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // =====================================================
            // GET ASSIGNMENTS
            // =====================================================

            var assignments = await _context.Assignments
                .Include(a => a.Trip)
                .Include(a => a.Driver)
                .Include(a => a.Vehicle)
                .OrderByDescending(a => a.AssignmentID)
                .ToListAsync();


            // =====================================================
            // GET AVAILABLE TRIPS
            // =====================================================

            ViewBag.Trips = await _context.Trips
                .Where(t =>
                    !_context.Assignments.Any(a =>
                        a.TripID == t.TripID &&
                        a.Status == "Assigned"))
                .OrderBy(t => t.TripID)
                .ToListAsync();


            // =====================================================
            // GET DRIVERS
            //
            // Show ALL drivers in the dropdown.
            // The Create method will still prevent using
            // a driver that is already assigned.
            // =====================================================

            ViewBag.Drivers = await _context.Drivers
                .OrderBy(d => d.FullName)
                .ToListAsync();


            // =====================================================
            // GET VEHICLES
            //
            // Show ALL vehicles in the dropdown.
            // The Create method will still prevent using
            // a vehicle that is already assigned.
            // =====================================================

            ViewBag.Vehicles = await _context.Vehicles
                .OrderBy(v => v.PlateNumber)
                .ToListAsync();


            return View(assignments);
        }


        // =========================================================
        // GET AVAILABLE TRIPS
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> GetAvailableTrips()
        {
            var trips = await _context.Trips
                .Where(t =>
                    !_context.Assignments.Any(a =>
                        a.TripID == t.TripID &&
                        a.Status == "Assigned"))
                .OrderBy(t => t.TripID)
                .Select(t => new
                {
                    tripID = t.TripID,
                    pickupLocation = t.PickupLocation,
                    destination = t.Destination,
                    departureTime = t.DepartureTime.ToString(),
                    arrivalTime = t.ArrivalTime.ToString()
                })
                .ToListAsync();

            return Json(trips);
        }


        // =========================================================
        // CREATE
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Create(
            int tripID,
            int driverID,
            int vehicleID,
            DateTime assignedDate,
            string status)
        {
            try
            {
                // =================================================
                // BASIC VALIDATION
                // =================================================

                if (tripID <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Please select a trip."
                    });
                }


                if (driverID <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Please select a driver."
                    });
                }


                if (vehicleID <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Please select a vehicle."
                    });
                }


                if (string.IsNullOrWhiteSpace(status))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Please select a status."
                    });
                }


                // =================================================
                // CHECK TRIP
                // =================================================

                var trip = await _context.Trips
                    .FirstOrDefaultAsync(t =>
                        t.TripID == tripID);

                if (trip == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected trip was not found."
                    });
                }


                // =================================================
                // CHECK IF TRIP IS ALREADY ASSIGNED
                // =================================================

                var tripAlreadyAssigned =
                    await _context.Assignments.AnyAsync(a =>
                        a.TripID == tripID &&
                        a.Status == "Assigned");

                if (tripAlreadyAssigned)
                {
                    return Json(new
                    {
                        success = false,
                        message = "This trip is already assigned."
                    });
                }


                // =================================================
                // CHECK DRIVER
                // =================================================

                var driver =
                    await _context.Drivers
                        .FirstOrDefaultAsync(d =>
                            d.DriverID == driverID);

                if (driver == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected driver was not found."
                    });
                }


                // =================================================
                // DRIVER MUST BE AVAILABLE
                // =================================================

                if (driver.Status != "Available")
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected driver is not available."
                    });
                }


                // =================================================
                // CHECK IF DRIVER IS ALREADY ASSIGNED
                // =================================================

                var driverAlreadyAssigned =
                    await _context.Assignments.AnyAsync(a =>
                        a.DriverID == driverID &&
                        a.Status == "Assigned");

                if (driverAlreadyAssigned)
                {
                    return Json(new
                    {
                        success = false,
                        message =
                            "Selected driver is already assigned to another trip."
                    });
                }


                // =================================================
                // CHECK VEHICLE
                // =================================================

                var vehicle =
                    await _context.Vehicles
                        .FirstOrDefaultAsync(v =>
                            v.VehicleID == vehicleID);

                if (vehicle == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected vehicle was not found."
                    });
                }


                // =================================================
                // CHECK IF VEHICLE IS ALREADY ASSIGNED
                // =================================================

                var vehicleAlreadyAssigned =
                    await _context.Assignments.AnyAsync(a =>
                        a.VehicleID == vehicleID &&
                        a.Status == "Assigned");

                if (vehicleAlreadyAssigned)
                {
                    return Json(new
                    {
                        success = false,
                        message =
                            "Selected vehicle is already assigned to another trip."
                    });
                }


                // =================================================
                // CREATE ASSIGNMENT
                // =================================================

                var assignment = new Assignment
                {
                    TripID = tripID,

                    DriverID = driverID,

                    VehicleID = vehicleID,

                    AssignedDate = assignedDate,

                    Status = status
                };


                _context.Assignments.Add(assignment);


                await _context.SaveChangesAsync();


                // =================================================
                // RETURN SUCCESS
                // =================================================

                return Json(new
                {
                    success = true,

                    assignmentId =
                        assignment.AssignmentID,

                    message =
                        "Assignment created successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,

                    message =
                        "Unable to create assignment.",

                    error =
                        ex.Message
                });
            }
        }


        // =========================================================
        // UPDATE
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Update(
            int assignmentID,
            int tripID,
            int driverID,
            int vehicleID,
            DateTime assignedDate,
            string status)
        {
            try
            {
                // =================================================
                // GET ASSIGNMENT
                // =================================================

                var assignment =
                    await _context.Assignments
                        .FirstOrDefaultAsync(a =>
                            a.AssignmentID == assignmentID);

                if (assignment == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Assignment not found."
                    });
                }


                // =================================================
                // CHECK TRIP
                // =================================================

                var tripExists =
                    await _context.Trips
                        .AnyAsync(t =>
                            t.TripID == tripID);

                if (!tripExists)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected trip was not found."
                    });
                }


                // =================================================
                // CHECK TRIP CONFLICT
                // =================================================

                var tripUsedByAnotherAssignment =
                    await _context.Assignments.AnyAsync(a =>
                        a.AssignmentID != assignmentID &&
                        a.TripID == tripID &&
                        a.Status == "Assigned");

                if (tripUsedByAnotherAssignment)
                {
                    return Json(new
                    {
                        success = false,
                        message =
                            "This trip is already assigned to another assignment."
                    });
                }


                // =================================================
                // CHECK DRIVER
                // =================================================

                var driver =
                    await _context.Drivers
                        .FirstOrDefaultAsync(d =>
                            d.DriverID == driverID);

                if (driver == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected driver was not found."
                    });
                }


                // =================================================
                // DRIVER AVAILABILITY
                // =================================================

                if (driver.Status != "Available")
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected driver is not available."
                    });
                }


                // =================================================
                // CHECK DRIVER CONFLICT
                // =================================================

                var driverUsedByAnotherAssignment =
                    await _context.Assignments.AnyAsync(a =>
                        a.AssignmentID != assignmentID &&
                        a.DriverID == driverID &&
                        a.Status == "Assigned");

                if (driverUsedByAnotherAssignment)
                {
                    return Json(new
                    {
                        success = false,
                        message =
                            "Selected driver is already assigned to another trip."
                    });
                }


                // =================================================
                // CHECK VEHICLE
                // =================================================

                var vehicleExists =
                    await _context.Vehicles
                        .AnyAsync(v =>
                            v.VehicleID == vehicleID);

                if (!vehicleExists)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Selected vehicle was not found."
                    });
                }


                // =================================================
                // CHECK VEHICLE CONFLICT
                // =================================================

                var vehicleUsedByAnotherAssignment =
                    await _context.Assignments.AnyAsync(a =>
                        a.AssignmentID != assignmentID &&
                        a.VehicleID == vehicleID &&
                        a.Status == "Assigned");

                if (vehicleUsedByAnotherAssignment)
                {
                    return Json(new
                    {
                        success = false,
                        message =
                            "Selected vehicle is already assigned to another trip."
                    });
                }


                // =================================================
                // UPDATE
                // =================================================

                assignment.TripID =
                    tripID;

                assignment.DriverID =
                    driverID;

                assignment.VehicleID =
                    vehicleID;

                assignment.AssignedDate =
                    assignedDate;

                assignment.Status =
                    status;


                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,

                    message =
                        "Assignment updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,

                    message =
                        "Unable to update assignment.",

                    error =
                        ex.Message
                });
            }
        }


        // =========================================================
        // DELETE
        // =========================================================

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var assignment =
                    await _context.Assignments
                        .FirstOrDefaultAsync(a =>
                            a.AssignmentID == id);

                if (assignment == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Assignment not found."
                    });
                }


                _context.Assignments.Remove(
                    assignment);


                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,

                    message =
                        "Assignment deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,

                    message =
                        "Unable to delete assignment.",

                    error =
                        ex.Message
                });
            }
        }
    }
}