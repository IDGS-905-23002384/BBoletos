using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentaBoletos.Models;

namespace VentaBoletos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Eventos (Devuelve eventos con sus respectivas zonas)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> GetEventos()
        {
            return await _context.Eventos
                .Include(e => e.Zonas) // Debe llamarse exactamente igual que en Evento.cs
                .ToListAsync();
        }
        // POST: api/Eventos (Permite crear el evento junto con sus zonas)
        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventos), new { id = evento.Id }, evento);
        }
    }
}