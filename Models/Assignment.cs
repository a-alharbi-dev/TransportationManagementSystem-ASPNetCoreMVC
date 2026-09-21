namespace TransportationManagementSystem.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }

        public int TripID { get; set; }

        public int DriverID { get; set; }

        public int VehicleID { get; set; }

        public DateTime AssignedDate { get; set; }

        public string Status { get; set; } = string.Empty;


        // Navigation Properties

        public Trip Trip { get; set; } = null!;

        public Driver Driver { get; set; } = null!;

        public Vehicle Vehicle { get; set; } = null!;
    }
}