using Gerenciador_de_Estoques_V2.Domain.Models;
using Sistema_de_Gerenciamento_de_Estoques.Infra.Context;
using Sistema_de_Gerenciamento_de_Estoques.Infra.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Sistema_de_Gerenciamento_de_Estoques.Infra.Repositories
{
    public class ProdutoRepository : IProdutoRepository<Produto>
    {
        private readonly ManagerContext _context;

        public ProdutoRepository(ManagerContext context)
        {
            _context = context;
        }

        public void AdicionarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void EditarProduto(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public List<Produto> ListarProdutos()
        {
            return _context.Produtos.ToList();
        }

        public void RemoverProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
        }
    }
}
