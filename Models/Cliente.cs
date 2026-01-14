using System.ComponentModel.DataAnnotations.Schema;

namespace Renta.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Column("id_cliente")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("telefono")]
        public string? Telefono { get; set; }
    }
}
