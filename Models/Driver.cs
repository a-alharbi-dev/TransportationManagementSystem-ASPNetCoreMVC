namespace TransportationManagementSystem.Models
{
    public class Driver
    {
        
        public int DriverID { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string LicenseNumber { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
