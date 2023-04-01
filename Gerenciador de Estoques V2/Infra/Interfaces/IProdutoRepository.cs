using System;
using System.Collections.Generic;

namespace Sistema_de_Gerenciamento_de_Estoques.Infra.Interfaces
{
    public interface IProdutoRepository<Produto> : IDisposable
    {
        void AdicionarProduto(Produto produto);
        List<Produto> ListarProdutos();
        void RemoverProduto(Produto produto);
        void EditarProduto(Produto produto);
    }
}
