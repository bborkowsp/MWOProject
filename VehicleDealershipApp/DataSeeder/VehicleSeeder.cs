using Bogus;
using SharedService.VehicleDealership;

namespace DataSeeder
{
    public class VehicleSeeder
    {
        public static List<Vehicle> GenerateVehicleData()
        {
            int productId = 1;
            var productFaker = new Faker<Vehicle>()
                .UseSeed(123456)
                .RuleFor(x => x.Model, x => x.Vehicle.Model())
                .RuleFor(x => x.Fuel, x => x.Vehicle.Fuel())
                .RuleFor(x => x.Id, x => productId++);

            return productFaker.Generate(50).ToList();
        }
    }
}