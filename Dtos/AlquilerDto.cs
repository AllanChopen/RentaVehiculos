using System;

namespace Renta.Dtos
{
    public class AlquilerDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string? ClienteNombre { get; set; }
        public int VehiculoId { get; set; }
        public string? VehiculoModelo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Total { get; set; }
    }
}
