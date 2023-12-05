using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SharedService.Services.VehicleDealershipService;
using SharedService.VehicleDealership;
using System.Diagnostics;

namespace P09ShopWebAPPMVC.Client.Controllers
{
    public class VehicleAPIController : Controller
    {

        private readonly IVehicleDealershipService _vehicleDealershipService;

        public VehicleAPIController(IVehicleDealershipService productService)
        {
            _vehicleDealershipService = productService;

        }

        // GET: VehicleDealership
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleDealershipService.GetVehiclesAsync();
            Debug.WriteLine("vehicles: " + vehicles.Success);
            return vehicles != null ?
                          View(vehicles.Data.AsEnumerable()) :
                          Problem("Entity set 'VehicleDealershipContext.VehicleDealership'  is null.");

            //return products != null ? 
            //              View("~/Views/VehicleDealership/Index.cshtml", products.Data.AsEnumerable()) :
            //              Problem("Entity set 'ShopContext.VehicleDealership'  is null.");
        }

        // GET: VehicleDealership/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var vehicle = await _vehicleDealershipService.GetVehicleByIdAsync((int)id);

            if (vehicle.Data == null)
            {
                return NotFound();
            }

            return View(vehicle.Data);
        }

        // GET: VehicleDealership/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleDealership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,Fuel")] Vehicle vehicle)
        {

            if (ModelState.IsValid)
            {
                await _vehicleDealershipService.CreateVehicleAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: VehicleDealership/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleDealershipService.GetVehicleByIdAsync((int)id);
            if (vehicle.Data == null)
            {
                return NotFound();
            }
            return View(vehicle.Data);
        }

        // POST: VehicleDealership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    var productResult = await _vehicleDealershipService.UpdateVehicleAsync(vehicle);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: VehicleDealership/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleDealershipService.GetVehicleByIdAsync((int)id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle.Data);
        }

        // POST: VehicleDealership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleDealershipService.DeleteVehicleAsync((int)id);
            if (vehicle.Success)
                return RedirectToAction(nameof(Index));
            else
                return NotFound();

        }

    }
}
