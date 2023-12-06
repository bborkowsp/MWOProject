using VehicleDealershipApp.VehicleDomain.Service.VehicleService;

using VehicleDealershipApp.VehicleDomain.Model;

using VehicleDealershipApp.VehicleDomain.Repository;


namespace VehicleDealershipApp.VehicleDomain.Service.VehicleService
{
    public class VehicleService : IVehicleService
    {

        VehicleRepository vehicleRepository;

        public VehicleService(VehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public Vehicle GetVehicle(int id)
        {
            return vehicleRepository.GetVehicleById(id);
        }

        public List<Vehicle> GetVehicles()
        {
            return vehicleRepository.GetAllVehicles();
        }

        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            return vehicleRepository.CreateVehicle(vehicle);
        }

        public bool UpdateVehicle(Vehicle vehicle)
        {
            return vehicleRepository.UpdateVehicle(vehicle.Id, vehicle);
        }

        public bool DeleteVehicle(int id)
        {
            return vehicleRepository.DeleteVehicle(id);
        }
    }
}
