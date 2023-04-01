using System.Collections.Generic;
using System.Linq;

namespace Gerenciador_de_Estoques_V2.Domain.Models
{

    public class Produto
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int Id { get; set; }

        public Produto(string nome, int quantidade, decimal preco)
        {
            Nome = nome;
            Quantidade = quantidade;
            Preco = preco;
        }
    }
    public class Models
    {
        private List<Produto> produtos;
        public Models()
        {
            produtos = new List<Produto>();
        }

        public void AdicionarProduto(Produto produto)
        {
            var produtos = new List<Produto>();
            produtos.Add(produto);
        }

        public List<Produto> ListarProdutos()
        {
            return produtos;
        }

        public void RemoverProduto(Produto produto)
        {
            produtos.Remove(produto);
        }

        public void EditarProduto(Produto produto)
        {
            var produtoExistente = produtos.FirstOrDefault(p => p.Nome == produto.Nome);
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Quantidade = produto.Quantidade;
            produtoExistente.Preco = produto.Preco;
        }
    }
}    
