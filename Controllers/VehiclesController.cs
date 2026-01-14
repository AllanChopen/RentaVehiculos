using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renta.Data;
using Renta.Models;

namespace Renta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VehiclesController(ApplicationDbContext db) => _db = db;
        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await _db.Vehicles.ToListAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            return vehicle is not null ? Ok(vehicle) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            _db.Vehicles.Add(vehicle);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, Vehicle update)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle is null) return NotFound();

            vehicle.Plate = update.Plate;
            vehicle.Model = update.Model;
            vehicle.Status = update.Status;
            vehicle.PricePerDay = update.PricePerDay;

            _db.Vehicles.Update(vehicle);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle is null) return NotFound();
            _db.Vehicles.Remove(vehicle);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
