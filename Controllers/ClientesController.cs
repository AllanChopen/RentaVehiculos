using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renta.Data;
using Renta.Models;

namespace Renta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ClientesController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Clientes.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Clientes.FindAsync(id);
            return item is not null ? Ok(item) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Cliente update)
        {
            var item = await _db.Clientes.FindAsync(id);
            if (item is null) return NotFound();

            item.Nombre = update.Nombre;
            item.Email = update.Email;
            item.Telefono = update.Telefono;

            _db.Clientes.Update(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.Clientes.FindAsync(id);
            if (item is null) return NotFound();
            _db.Clientes.Remove(item);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
