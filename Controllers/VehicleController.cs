using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class VehicleController : Controller
    {
        private readonly TransportationDbContext _context;

        public VehicleController(TransportationDbContext context)
        {
            _context = context;
        }


        // ==========================================
        // VEHICLE MANAGEMENT PAGE
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _context.Vehicles
                .OrderBy(v => v.VehicleID)
                .ToListAsync();

            return View("~/Views/Manager/VehicleManagement.cshtml", vehicles);
        }


        // ==========================================
        // CREATE VEHICLE
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Create(
            string plateNumber,
            string vehicleType,
            string model,
            int capacity,
            string status)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(plateNumber))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Plate number is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(vehicleType))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vehicle type is required."
                    });
                }

                if (string.IsNullOrWhiteSpace(model))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Model is required."
                    });
                }

                if (capacity <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Capacity must be greater than zero."
                    });
                }

                if (string.IsNullOrWhiteSpace(status))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Status is required."
                    });
                }


                var vehicle = new Vehicle
                {
                    PlateNumber = plateNumber,
                    VehicleType = vehicleType,
                    Model = model,
                    Capacity = capacity,
                    Status = status
                };


                _context.Vehicles.Add(vehicle);

                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Vehicle created successfully.",
                    vehicleId = vehicle.VehicleID
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to create vehicle.",
                    error = ex.Message
                });
            }
        }


        // ==========================================
        // UPDATE VEHICLE
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Update(
            [FromBody] Vehicle vehicle)
        {
            try
            {
                if (vehicle.VehicleID <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vehicle ID is invalid."
                    });
                }


                var existingVehicle =
                    await _context.Vehicles
                        .FirstOrDefaultAsync(
                            v => v.VehicleID == vehicle.VehicleID);


                if (existingVehicle == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vehicle not found."
                    });
                }


                existingVehicle.PlateNumber =
                    vehicle.PlateNumber;

                existingVehicle.VehicleType =
                    vehicle.VehicleType;

                existingVehicle.Model =
                    vehicle.Model;

                existingVehicle.Capacity =
                    vehicle.Capacity;

                existingVehicle.Status =
                    vehicle.Status;


                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Vehicle updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to update vehicle.",
                    error = ex.Message
                });
            }
        }


        // ==========================================
        // DELETE VEHICLE
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var vehicle =
                    await _context.Vehicles
                        .FirstOrDefaultAsync(
                            v => v.VehicleID == id);


                if (vehicle == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Vehicle not found."
                    });
                }


                _context.Vehicles.Remove(vehicle);

                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Vehicle deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete vehicle.",
                    error = ex.Message
                });
            }
        }
    }
}
