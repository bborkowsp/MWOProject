using Microsoft.EntityFrameworkCore;
using SharedService.VehicleDealership;
using DataSeeder;

namespace API.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent api 
            // modelBuilder.Entity<Vehicle>()
            //     .Property(p => p.Barcode)
            //     .IsRequired()
            //     .HasMaxLength(12);

            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Model)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Fuel)
                .IsRequired()
                .HasMaxLength(100);

            // modelBuilder.Entity<Vehicle>()
            //  .Property(p => p.Price)
            //  .HasColumnType("decimal(8,2)");

            // data seed 

            modelBuilder.Entity<Vehicle>().HasData(VehicleSeeder.GenerateVehicleData());
        }
    }
}
// instalacja dotnet ef 
//dotnet tool install --global dotnet-ef

// aktualizacja 
//dotnet tool update --global dotnet-ef

// dotnet ef migrations add [nazwa_migracji]
// dotnet ef database update 


// cofniecie migraji niezaplikowanych 
//dotnet ef migrations remove