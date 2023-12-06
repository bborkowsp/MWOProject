using VehicleDealershipApp.VehicleDomain.Model;

namespace VehicleDealershipApp.VehicleDomain.Repository
{
    public interface IVehicleRepository
    {

        public List<Vehicle> GetAllVehicles();
        Vehicle GetVehicleById(int id);
        Vehicle CreateVehicle(Vehicle vehicle);
        bool UpdateVehicle(int id, Vehicle updatedVehicle);
        bool DeleteVehicle(int id);
    }
}