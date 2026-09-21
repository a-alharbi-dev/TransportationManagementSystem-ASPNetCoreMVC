namespace TransportationManagementSystem.Models
{
    public class Trip
    {
        public int TripID { get; set; }

        public string PickupLocation { get; set; } = string.Empty;

        public string Destination { get; set; } = string.Empty;

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        // Navigation Property
        public ICollection<Assignment> Assignments { get; set; }
            = new List<Assignment>();
    }
}