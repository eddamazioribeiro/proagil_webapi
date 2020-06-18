using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.WebAPI.Data
{
    public class ProAgilContext : DbContext
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base(options) {}

        public DbSet<Evento> Eventos {get; set;}
        public DbSet<Palestrante> Palestrantes {get; set;}
        public DbSet<PalestranteEvento> PalestrantesEventos {get; set;}
        public DbSet<Lote> Lotes {get; set;}
        public DbSet<RedeSocial> RedeSociais {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // N:N relation on PalestranteEvento
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(pe => new {pe.EventoId, pe.PalestranteId});
        }
    }
}