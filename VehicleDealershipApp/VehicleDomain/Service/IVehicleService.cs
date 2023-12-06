using VehicleDealershipApp.VehicleDomain.Model;

namespace VehicleDealershipApp.VehicleDomain.Service.VehicleService
{
    public interface IVehicleService
    {
        Vehicle GetVehicle(int id);
        List<Vehicle> GetVehicles();

        Vehicle CreateVehicle(Vehicle product);

        bool UpdateVehicle(Vehicle product);

        bool DeleteVehicle(int id);

    }
}
