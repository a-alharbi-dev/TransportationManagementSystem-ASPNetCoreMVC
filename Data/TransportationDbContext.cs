using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Models;

namespace TransportationManagementSystem.Data
{
    public class TransportationDbContext : DbContext
    {
        public TransportationDbContext(
            DbContextOptions<TransportationDbContext> options)
            : base(options)
        {
        }

        // ==========================================
        // DATABASE TABLES
        // ==========================================

        public DbSet<User> Users { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<EmergencyRequest> EmergencyRequests { get; set; }

        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ==========================================
            // PRIMARY KEYS
            // ==========================================

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserID);

            modelBuilder.Entity<Driver>()
                .HasKey(d => d.DriverID);

            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.VehicleID);

            modelBuilder.Entity<Trip>()
                .HasKey(t => t.TripID);

            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.AssignmentID);

            modelBuilder.Entity<EmergencyRequest>()
                .HasKey(e => e.RequestID);

            modelBuilder.Entity<Notification>()
                .HasKey(n => n.NotificationID);


            // ==========================================
            // TRIP
            // ==========================================

            modelBuilder.Entity<Trip>()
                .Property(t => t.PickupLocation)
                .IsRequired();

            modelBuilder.Entity<Trip>()
                .Property(t => t.Destination)
                .IsRequired();

            modelBuilder.Entity<Trip>()
                .Property(t => t.DepartureTime)
                .IsRequired();

            modelBuilder.Entity<Trip>()
                .Property(t => t.ArrivalTime)
                .IsRequired();


            // ==========================================
            // ASSIGNMENT → TRIP
            // ==========================================

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Trip)
                .WithMany(t => t.Assignments)
                .HasForeignKey(a => a.TripID)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // ASSIGNMENT → DRIVER
            // ==========================================

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Driver)
                .WithMany()
                .HasForeignKey(a => a.DriverID)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // ASSIGNMENT → VEHICLE
            // ==========================================

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Vehicle)
                .WithMany()
                .HasForeignKey(a => a.VehicleID)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // EMERGENCY REQUEST → USER
            // ==========================================

            modelBuilder.Entity<EmergencyRequest>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // EMERGENCY REQUEST → DRIVER
            // ==========================================

            modelBuilder.Entity<EmergencyRequest>()
                .HasOne(e => e.Driver)
                .WithMany()
                .HasForeignKey(e => e.DriverID)
                .OnDelete(DeleteBehavior.Restrict);


            // ==========================================
            // EMERGENCY REQUEST → VEHICLE
            // ==========================================

            modelBuilder.Entity<EmergencyRequest>()
                .HasOne(e => e.Vehicle)
                .WithMany()
                .HasForeignKey(e => e.VehicleID)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // NOTIFICATION → USER
            // ==========================================

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}