using Microsoft.EntityFrameworkCore;
using Renta.Models;

namespace Renta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Alquiler> Alquileres { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().ToTable("vehiculos");
            modelBuilder.Entity<Cliente>().ToTable("clientes");
            modelBuilder.Entity<Alquiler>().ToTable("alquileres");
            modelBuilder.Entity<Pago>().ToTable("pagos");
            base.OnModelCreating(modelBuilder);
        }
    }
}
