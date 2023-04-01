using Gerenciador_de_Estoques_V2.Domain.Models;
using System.Linq;

namespace GerenciadorDeEstoque.Filtros
{
    public class FiltroPorQuantidade : FiltroBase<Produto>
    {
        private readonly decimal _QntMinima;
        private readonly decimal _QntMaxima;
        public FiltroPorQuantidade(decimal QntMinima, decimal QntMaxima)
        {
            _QntMinima = QntMinima;
            _QntMaxima = QntMaxima;
        }

        public override IQueryable<Produto> Aplicar(IQueryable<Produto> produtos)
        {
            return produtos.Where(p => p.Quantidade >= _QntMinima && p.Quantidade <= _QntMaxima);
        }

    }
}
