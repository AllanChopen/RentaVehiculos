using System.ComponentModel.DataAnnotations.Schema;

namespace Renta.Models
{
    [Table("pagos")]
    public class Pago
    {
        [Column("id_pago")]
        public int Id { get; set; }

        [Column("id_alquiler")]
        public int AlquilerId { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; }

        [Column("metodo_pago")]
        public string MetodoPago { get; set; } = string.Empty;
    }
}
