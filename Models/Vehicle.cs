namespace TransportationManagementSystem.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        public string PlateNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}