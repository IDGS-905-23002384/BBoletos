using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentaBoletos.Models;

namespace VentaBoletos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BoletosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Boletos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Boleto>>> GetBoletos()
        {
            return await _context.Boletos
                .Include(b => b.ZonaEvento)
                .ThenInclude(z => z.Evento)
                .ToListAsync();
        }

        // GET: api/Boletos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Boleto>> GetBoleto(int id)
        {
            var boleto = await _context.Boletos
                .Include(b => b.ZonaEvento)
                .ThenInclude(z => z.Evento)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            return boleto;
        }

        // POST: api/Boletos (Comprar boletos restando el inventario de la zona)
        [HttpPost]
        public async Task<ActionResult<Boleto>> ComprarBoleto(Boleto boleto)
        {
            var zona = await _context.ZonasEventos.FindAsync(boleto.ZonaEventoId);
            if (zona == null)
            {
                return NotFound(new { mensaje = "La zona seleccionada no existe." });
            }

            if (boleto.Cantidad <= 0)
            {
                return BadRequest(new { mensaje = "Debes comprar al menos 1 boleto." });
            }

            if (boleto.Cantidad > zona.LugaresDisponibles)
            {
                return BadRequest(new { mensaje = $"Lo sentimos, solo quedan {zona.LugaresDisponibles} lugares disponibles en esta zona." });
            }

            // --- CÁLCULOS FINANCIEROS Y DE IVA (16%) ---
            decimal precioUnitario = zona.Precio;
            decimal subtotal = precioUnitario * boleto.Cantidad;
            decimal iva = subtotal * 0.16m;
            decimal totalPagado = subtotal + iva;

            // Asignar los montos calculados al modelo boleto
            boleto.PrecioUnitario = precioUnitario;
            boleto.Subtotal = subtotal;
            boleto.IVA = iva;
            boleto.TotalPagado = totalPagado;
            boleto.FechaCompra = DateTime.Now;

            // Descontar inventario de la zona
            zona.LugaresDisponibles -= boleto.Cantidad;

            _context.Boletos.Add(boleto);
            _context.Entry(zona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoleto), new { id = boleto.Id }, boleto);
        }
    }
}