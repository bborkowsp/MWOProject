using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services.VehicleDealershipService
{
    public class VehicleDealershipService : IVehicleDealershipService
    {
        private readonly DataContext _dataContext;
        public VehicleDealershipService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _dataContext.Vehicles.Add(vehicle);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Vehicle>() { Data = vehicle, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>()
                {
                    Data = null,
                    Success = false,
                    Message = "Cannot create vehicle"
                };
            }

        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {


            // sposób 2: (uzywamy attach : tylko jedno zapytanie do bazy)
            var vehicle = new Vehicle() { Id = id };
            _dataContext.Vehicles.Attach(vehicle);
            _dataContext.Vehicles.Remove(vehicle);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true, Success = true };
        }

        public async Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _dataContext.Vehicles.FindAsync(id);
                var response = new ServiceResponse<Vehicle>()
                {
                    Data = vehicle,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var vehicles = await _dataContext.Vehicles.ToListAsync();

            try
            {
                var response = new ServiceResponse<List<Vehicle>>()
                {
                    Data = vehicles,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Vehicle>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }

        }

        public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                var vehicleToEdit = new Vehicle() { Id = vehicle.Id };
                _dataContext.Vehicles.Attach(vehicleToEdit);

                vehicleToEdit.Model = vehicle.Model;
                vehicleToEdit.Fuel = vehicle.Fuel;



                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = vehicleToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>
                {
                    Data = null,
                    Success = false,
                    Message = "An error occured while updating vehicle"
                };
            }



        }
    }
}
