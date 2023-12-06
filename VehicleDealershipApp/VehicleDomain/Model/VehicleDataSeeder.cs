using Bogus;

namespace VehicleDealershipApp.VehicleDomain.Model
{
    public class VehicleDataSeeder
    {
        public static List<Vehicle> GenerateVehicleData()
        {
            int vehicleId = 1;
            var vehicleDataFaker = new Faker<Vehicle>()
                .UseSeed(123456)
                .RuleFor(x => x.Model, x => x.Vehicle.Model())
                .RuleFor(x => x.Fuel, x => x.Vehicle.Fuel())
                .RuleFor(x => x.Id, x => vehicleId++);

            return vehicleDataFaker.Generate(50).ToList();
        }
    }
}