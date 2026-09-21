using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Controllers
{
    public class DriverController : Controller
    {
        private readonly TransportationDbContext _context;

        public DriverController(TransportationDbContext context)
        {
            _context = context;
        }


        // ==========================================
        // DRIVER MANAGEMENT PAGE
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers
                .OrderBy(d => d.DriverID)
                .ToListAsync();

            return View("~/Views/Manager/DriverManagement.cshtml", drivers);
        }


        // ==========================================
        // CREATE DRIVER
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Create(
            string fullName,
            string phone,
            string licenseNumber,
            string status)
        {
            try
            {
                // ------------------------------------------
                // VALIDATION
                // ------------------------------------------

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver name is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(phone))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Phone number is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(licenseNumber))
                {
                    return Json(new
                    {
                        success = false,
                        message = "License number is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(status))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver status is required."
                    });
                }


                // ------------------------------------------
                // CHECK DUPLICATE LICENSE
                // ------------------------------------------

                bool licenseExists = await _context.Drivers
                    .AnyAsync(d =>
                        d.LicenseNumber == licenseNumber);

                if (licenseExists)
                {
                    return Json(new
                    {
                        success = false,
                        message = "This license number already exists."
                    });
                }


                // ------------------------------------------
                // CREATE DRIVER
                // ------------------------------------------

                var driver = new Driver
                {
                    FullName = fullName,
                    Phone = phone,
                    LicenseNumber = licenseNumber,
                    Status = status
                };


                _context.Drivers.Add(driver);

                await _context.SaveChangesAsync();


                // ------------------------------------------
                // SUCCESS
                // ------------------------------------------

                return Json(new
                {
                    success = true,
                    message = "Driver created successfully.",
                    driverId = driver.DriverID
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to create driver.",
                    error = ex.Message
                });
            }
        }


        // ==========================================
        // UPDATE DRIVER
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Update(
            int driverID,
            string fullName,
            string phone,
            string licenseNumber,
            string status)
        {
            try
            {
                // ------------------------------------------
                // FIND DRIVER
                // ------------------------------------------

                var driver = await _context.Drivers
                    .FirstOrDefaultAsync(d =>
                        d.DriverID == driverID);


                if (driver == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver not found."
                    });
                }


                // ------------------------------------------
                // VALIDATION
                // ------------------------------------------

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver name is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(phone))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Phone number is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(licenseNumber))
                {
                    return Json(new
                    {
                        success = false,
                        message = "License number is required."
                    });
                }


                if (string.IsNullOrWhiteSpace(status))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver status is required."
                    });
                }


                // ------------------------------------------
                // CHECK DUPLICATE LICENSE
                // ------------------------------------------

                bool licenseExists = await _context.Drivers
                    .AnyAsync(d =>
                        d.LicenseNumber == licenseNumber &&
                        d.DriverID != driverID);


                if (licenseExists)
                {
                    return Json(new
                    {
                        success = false,
                        message = "This license number already exists."
                    });
                }


                // ------------------------------------------
                // UPDATE
                // ------------------------------------------

                driver.FullName = fullName;

                driver.Phone = phone;

                driver.LicenseNumber = licenseNumber;

                driver.Status = status;


                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Driver updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to update driver.",
                    error = ex.Message
                });
            }
        }


        // ==========================================
        // DELETE DRIVER
        // ==========================================

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var driver = await _context.Drivers
                    .FirstOrDefaultAsync(d =>
                        d.DriverID == id);


                if (driver == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Driver not found."
                    });
                }


                _context.Drivers.Remove(driver);

                await _context.SaveChangesAsync();


                return Json(new
                {
                    success = true,
                    message = "Driver deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete driver.",
                    error = ex.Message
                });
            }
        }
    }
}