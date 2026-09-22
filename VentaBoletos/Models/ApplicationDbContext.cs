using Microsoft.EntityFrameworkCore;

namespace VentaBoletos.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<ZonaEvento> ZonasEventos { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la precisión para los campos decimales y evitar advertencias o truncamientos
            modelBuilder.Entity<ZonaEvento>()
                .Property(z => z.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Boleto>()
                .Property(b => b.TotalPagado)
                .HasPrecision(18, 2);
        }
    }
}