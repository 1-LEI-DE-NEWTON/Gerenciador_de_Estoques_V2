using System.Linq;

namespace GerenciadorDeEstoque.Filtros
{
    public abstract class FiltroBase<T>
    {
        public abstract IQueryable<T> Aplicar(IQueryable<T> query);
    }
}
