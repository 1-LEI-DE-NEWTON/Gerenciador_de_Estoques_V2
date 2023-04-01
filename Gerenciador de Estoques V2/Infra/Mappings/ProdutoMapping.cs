using Gerenciador_de_Estoques_V2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema_de_Gerenciamento_de_Estoques.Infra.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.
                HasKey(p => p.Id);

            builder.
                Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.
                Property(p => p.Quantidade)
                .HasColumnType("int")
                .IsRequired();

            builder.
                Property(p => p.Preco)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
