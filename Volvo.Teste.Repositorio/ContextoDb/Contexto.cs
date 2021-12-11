using Microsoft.EntityFrameworkCore;
using Volvo.Teste.Dominio;

namespace Volvo.Teste.Repositorio.ContextoDb
{
    public class Contexto : DbContext
    {
        public Contexto()
        { }

        public Contexto(DbContextOptions<Contexto> opcoes)
            : base(opcoes)
        { }

        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Caminhao>()
                        .Property(f => f.Id)
                        .ValueGeneratedOnAdd();

            builder.Entity<Marca>()
                       .Property(f => f.Id)
                       .ValueGeneratedOnAdd();

            builder.Entity<Modelo>()
                       .Property(f => f.Id)
                       .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);


        }
    }
}
