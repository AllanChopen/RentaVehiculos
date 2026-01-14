using System;

namespace Renta.Dtos
{
    public class CreateAlquilerDto
    {
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
