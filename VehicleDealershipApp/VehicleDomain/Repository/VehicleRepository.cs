using System.Diagnostics;

using VehicleDealershipApp.VehicleDomain.Model;

namespace VehicleDealershipApp.VehicleDomain.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        Dictionary<int, Vehicle> vehicles = new Dictionary<int, Vehicle>();
        int vehicleId = 1;

        public VehicleRepository() 
        {
        }

        public VehicleRepository(List<Vehicle> vehicles) 
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                this.vehicles.Add(vehicles[i].Id, vehicles[i]);
                vehicleId++;
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            if (vehicles.TryGetValue(id, out var vehicle))
            {
                return vehicle;
            }
            return null;
        }

        public List<Vehicle> GetAllVehicles()
        {
            return vehicles.Values.ToList();
        }


        public Vehicle CreateVehicle(Vehicle vehicle)
        {

            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            vehicle.Id = vehicleId++;
            vehicles.Add(vehicle.Id, vehicle);
            

            return vehicle;
        }



        public bool UpdateVehicle(int id, Vehicle updatedVehicle)
        {
            if (vehicles.TryGetValue(id, out var vehicleToEdit))
            {
                vehicleToEdit.Model = updatedVehicle.Model;
                vehicleToEdit.Fuel = updatedVehicle.Fuel;
                return true;
            }

            return false;
        }

        public bool DeleteVehicle(int id)
        {
            if (vehicles.ContainsKey(id))
            {
                vehicles.Remove(id);
                GetAllVehicles();
                return true;
            }
            return false;
        }
    }
}