using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportationManagementSystem.Models
{
    public class EmergencyRequest
    {
        [Key]
        public int RequestID { get; set; }

        // ==========================================
        // USER
        // ==========================================

        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User? User { get; set; }


        // ==========================================
        // REQUEST INFORMATION
        // ==========================================

        public string PickupLocation { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;

        public string Priority { get; set; } = string.Empty;

        public DateTime RequestTime { get; set; }

        public string Reason { get; set; } = string.Empty;

        public string? Notes { get; set; }


        // ==========================================
        // STATUS
        // ==========================================

        public string Status { get; set; } = "Pending";


        // ==========================================
        // DRIVER
        // ==========================================

        public int? DriverID { get; set; }

        [ForeignKey("DriverID")]
        public Driver? Driver { get; set; }


        // ==========================================
        // VEHICLE
        // ==========================================

        public int? VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle? Vehicle { get; set; }
    }
}