
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P06Shop.Shared;
using P06Shop.Shared.Configuration;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Services.VehicleDealershipService
{
    public class VehicleDealershipService : IVehicleDealershipService
    {

        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public VehicleDealershipService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var response = await _httpClient.PostAsJsonAsync(url, vehicle);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {
            // jesli uzyjemy / na poczatku to wtedy sciezka trakktowana jest od root czyli pomija czesc środkową adresu 
            // zazwyczaj unikamy stosowania / na poczatku 
            //var response = await _httpClient.DeleteAsync($"{id}");
            var url = $"{_appSettings.BaseAPIUrl}/{_appSettings.VehicleDealershipEndpoints.DeleteVehicleEndpoint.Replace("{vehicleId}", id.ToString())}";
            var response = await _httpClient.DeleteAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result;
        }


        // alternatywny sposób pobierania danych 
        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "HTTP request failed"
                };

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(json)
                ?? new ServiceResponse<List<Vehicle>> { Success = false, Message = "Deserialization failed" };

            result.Success = result.Success && result.Data != null;

            return result;
        }

        public async Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            var url = $"{_appSettings.BaseAPIUrl}/{_appSettings.VehicleDealershipEndpoints.GetVehicleEndpoint.Replace("{vehicleId}", id.ToString())}";
            var response = await _httpClient.GetAsync(url);
            //var response = await _httpClient.GetAsync(id.ToString());
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse<Vehicle>
                {
                    Success = false,
                    Message = "HTTP request failed"
                };

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<Vehicle>>(json)
                ?? new ServiceResponse<Vehicle> { Success = false, Message = "Deserialization failed" };

            result.Success = result.Success && result.Data != null;

            return result;
        }


        // wersja 2 
        public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }
    }
}
