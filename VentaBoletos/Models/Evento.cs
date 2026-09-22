using VentaBoletos.Models;

public class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaHora { get; set; }
    public string Lugar { get; set; }

    // Debe llamarse exactamente Zonas para que coincida con el controlador y el envío
    public List<ZonaEvento> Zonas { get; set; } = new List<ZonaEvento>();
}