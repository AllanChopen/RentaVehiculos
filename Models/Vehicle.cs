using System.ComponentModel.DataAnnotations.Schema;

namespace Renta.Models
{
    [Table("vehiculos")]
    public class Vehicle
    {
        [Column("id_vehiculo")]
        public int Id { get; set; }

        [Column("placa")]
        public string Plate { get; set; } = string.Empty;

        [Column("modelo")]
        public string Model { get; set; } = string.Empty;

        [Column("estado")]
        public string Status { get; set; } = string.Empty;

        [Column("precio_por_dia")]
        public decimal PricePerDay { get; set; }
    }
}
