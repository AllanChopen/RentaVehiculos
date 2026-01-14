using System.ComponentModel.DataAnnotations.Schema;

namespace Renta.Models
{
    [Table("alquileres")]
    public class Alquiler
    {
        [Column("id_alquiler")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("id_cliente")]
        public int ClienteId { get; set; }

        [Column("id_vehiculo")]
        public int VehiculoId { get; set; }

        [Column("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin")]
        public DateTime FechaFin { get; set; }

        [Column("total")]
        public decimal Total { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        [ForeignKey(nameof(VehiculoId))]
        public Vehicle? Vehiculo { get; set; }
    }
}
