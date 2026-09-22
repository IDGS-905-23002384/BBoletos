using System.Text.Json.Serialization;

namespace VentaBoletos.Models
{
    public class ZonaEvento
    {
        public int Id { get; set; }
        public string NombreZona { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int LugaresDisponibles { get; set; }

        public int EventoId { get; set; }

        [JsonIgnore] // <--- Esto evita que se cree el ciclo infinito al convertir a JSON
        public Evento? Evento { get; set; }
    }
}