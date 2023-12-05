using P06Shop.Shared;
using P06Shop.Shared.VehicleDealership;

namespace P06Shop.Shared.Services.VehicleDealershipService
{
    public interface IVehicleDealershipService
    {
        Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync();

        Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle product);

        Task<ServiceResponse<bool>> DeleteVehicleAsync(int id);

        Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle product);

        Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id);
    }
}
