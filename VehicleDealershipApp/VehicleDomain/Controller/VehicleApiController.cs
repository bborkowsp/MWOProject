using Microsoft.AspNetCore.Mvc;

using VehicleDealershipApp.VehicleDomain.Model;
using VehicleDealershipApp.VehicleDomain.Service.VehicleService;

namespace VehicleDealershipApp.VehicleDomain.Controller
{
    public class VehicleApiController : Microsoft.AspNetCore.Mvc.Controller
    {
      
        private readonly IVehicleService _vehicleService;

        public VehicleApiController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = _vehicleService.GetVehicles();
            return vehicles != null ?
                          View(vehicles.AsEnumerable()) :
                          Problem("Empty repository");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Fuel")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _vehicleService.CreateVehicle(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _vehicleService.GetVehicle((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Fuel")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productResult = _vehicleService.UpdateVehicle(vehicle);
                }
                catch (Exception)
                {
                     return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = _vehicleService.GetVehicle((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = _vehicleService.DeleteVehicle(id);
            if (vehicle)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vehicle = _vehicleService.GetVehicle((int)id);
            
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
    }
}
