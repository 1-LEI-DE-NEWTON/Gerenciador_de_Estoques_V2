using Gerenciador_de_Estoques_V2.Domain.Models;
using System.Linq;

namespace GerenciadorDeEstoque.Filtros;
public class FiltroPreco : FiltroBase<Produto>
{
    private readonly decimal _precoMinimo;
    private readonly decimal _precoMaximo;
    public FiltroPreco(decimal PrecoMinimo, decimal PrecoMaximo)
    {
        _precoMinimo = PrecoMinimo;
        _precoMaximo = PrecoMaximo;
    }

    public override IQueryable<Produto> Aplicar(IQueryable<Produto> produtos)
    {
        return produtos.Where(p => p.Preco >= _precoMinimo && p.Preco <= _precoMaximo);
    }
}
