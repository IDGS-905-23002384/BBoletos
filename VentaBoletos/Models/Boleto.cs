using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentaBoletos.Models
{
    public class Boleto
    {
        public int Id { get; set; }

        [Required]
        public string CompradorNombre { get; set; } = string.Empty;

        [Required]
        public string CompradorEmail { get; set; } = string.Empty;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }

        public decimal TotalPagado { get; set; }

        public DateTime FechaCompra { get; set; } = DateTime.Now;

        // Llave foránea hacia ZonaEvento
        public int ZonaEventoId { get; set; }

        [ForeignKey("ZonaEventoId")]
        public ZonaEvento? ZonaEvento { get; set; }
    }
}