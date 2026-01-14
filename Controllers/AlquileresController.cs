using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renta.Data;
using Renta.Models;
using Renta.Dtos;

namespace Renta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlquileresController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AlquileresController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Vehiculo)
                .Select(a => new AlquilerDto {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ClienteNombre = a.Cliente != null ? a.Cliente.Nombre : null,
                    VehiculoId = a.VehiculoId,
                    VehiculoModelo = a.Vehiculo != null ? a.Vehiculo.Model : null,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    Total = a.Total
                })
                .ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Vehiculo)
                .Where(a => a.Id == id)
                .Select(a => new AlquilerDto {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ClienteNombre = a.Cliente != null ? a.Cliente.Nombre : null,
                    VehiculoId = a.VehiculoId,
                    VehiculoModelo = a.Vehiculo != null ? a.Vehiculo.Model : null,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    Total = a.Total
                })
                .FirstOrDefaultAsync();

            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlquilerDto dto)
        {
            var vehicle = await _db.Vehicles.FindAsync(dto.VehiculoId);
            var cliente = await _db.Clientes.FindAsync(dto.ClienteId);
            if (vehicle is null) return BadRequest($"Vehiculo {dto.VehiculoId} not found");
            if (cliente is null) return BadRequest($"Cliente {dto.ClienteId} not found");

            var start = dto.FechaInicio.Date;
            var end = dto.FechaFin.Date;
            var days = (end - start).Days + 1;
            if (days <= 0) return BadRequest("FechaFin must be on or after FechaInicio");

            var alquiler = new Alquiler {
                ClienteId = dto.ClienteId,
                VehiculoId = dto.VehiculoId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Total = vehicle.PricePerDay * days
            };

            _db.Alquileres.Add(alquiler);
            await _db.SaveChangesAsync();

            var created = await _db.Alquileres
                .Include(a => a.Cliente)
                .Include(a => a.Vehiculo)
                .Where(a => a.Id == alquiler.Id)
                .Select(a => new AlquilerDto {
                    Id = a.Id,
                    ClienteId = a.ClienteId,
                    ClienteNombre = a.Cliente != null ? a.Cliente.Nombre : null,
                    VehiculoId = a.VehiculoId,
                    VehiculoModelo = a.Vehiculo != null ? a.Vehiculo.Model : null,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    Total = a.Total
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(Get), new { id = alquiler.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Alquiler update)
        {
            var item = await _db.Alquileres.FindAsync(id);
            if (item is null) return NotFound();

            item.ClienteId = update.ClienteId;
            item.VehiculoId = update.VehiculoId;
            item.FechaInicio = update.FechaInicio;
            item.FechaFin = update.FechaFin;
            item.Total = update.Total;

            _db.Alquileres.Update(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Alquileres.FindAsync(id);
            if (item is null) return NotFound();
            _db.Alquileres.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
