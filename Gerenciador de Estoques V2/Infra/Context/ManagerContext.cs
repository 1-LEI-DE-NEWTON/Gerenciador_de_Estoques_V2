using Microsoft.EntityFrameworkCore;
using Sistema_de_Gerenciamento_de_Estoques.Infra.Mappings;
using Gerenciador_de_Estoques_V2.Domain.Models;

namespace Sistema_de_Gerenciamento_de_Estoques.Infra.Context
{
    public class ManagerContext : DbContext
    {
        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMapping());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=localhost;database=gerenciadordeestoque;uid=root";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }

}
