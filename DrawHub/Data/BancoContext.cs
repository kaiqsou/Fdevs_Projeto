using DrawHub.Data.Maps;
using DrawHub.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawHub.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Desenho> Desenhos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        // Passando o mapeamento de desenho para as configurações do ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DesenhoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}