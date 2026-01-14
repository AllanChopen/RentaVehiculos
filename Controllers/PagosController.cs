using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renta.Data;
using Renta.Models;

namespace Renta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PagosController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Pagos.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Pagos.FindAsync(id);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pago pago)
        {
            _db.Pagos.Add(pago);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = pago.Id }, pago);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Pago update)
        {
            var item = await _db.Pagos.FindAsync(id);
            if (item is null) return NotFound();

            item.AlquilerId = update.AlquilerId;
            item.Monto = update.Monto;
            item.FechaPago = update.FechaPago;
            item.MetodoPago = update.MetodoPago;

            _db.Pagos.Update(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Pagos.FindAsync(id);
            if (item is null) return NotFound();
            _db.Pagos.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
