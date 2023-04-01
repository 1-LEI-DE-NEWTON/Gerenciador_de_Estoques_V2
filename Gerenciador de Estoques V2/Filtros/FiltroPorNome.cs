using Gerenciador_de_Estoques_V2.Domain.Models;
using System.Linq;

namespace GerenciadorDeEstoque.Filtros;
public class FiltroPorNome : FiltroBase<Produto>
{
    private readonly string _nome;

    public FiltroPorNome(string nome)
    {
        _nome = nome;
    }

    public override IQueryable<Produto> Aplicar(IQueryable<Produto> query)
    {
        if (string.IsNullOrEmpty(_nome))
        {
            return query;
        }

        return query.Where(p => p.Nome.Contains(_nome));
    }
}
