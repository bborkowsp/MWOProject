using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedService;
using SharedService.Services.VehicleDealershipService;
using SharedService.VehicleDealership;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleDealershipService _vehicleDealershipService;
        public VehicleController(IVehicleDealershipService vehicleDealershipService)
        {
            _vehicleDealershipService = vehicleDealershipService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> GetVehicles()
        {

            var result = await _vehicleDealershipService.GetVehiclesAsync();

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


        // http:localhost/api/product/4 dla api REST
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetVehicle(int id)
        {

            var result = await _vehicleDealershipService.GetVehicleByIdAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> UpdateVehicle([FromBody] Vehicle product)
        {

            var result = await _vehicleDealershipService.UpdateVehicleAsync(product);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> CreateVehicle([FromBody] Vehicle product)
        {
            var result = await _vehicleDealershipService.CreateVehicleAsync(product);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        // http:localhost/api/product/delete?id=4
        // http:localhost/api/product/4 dla api REST
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteVehicle([FromRoute] int id)
        {
            var result = await _vehicleDealershipService.DeleteVehicleAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }



    }
}
