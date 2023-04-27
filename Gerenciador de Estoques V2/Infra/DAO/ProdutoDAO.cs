using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Gerenciador_de_Estoques_V2.Domain.Models;
using GerenciadorDeEstoque.Filtros;
using MySql.Data.MySqlClient;

namespace Sistema_de_Gerenciamento_de_Estoques.Infra.DAO
{    
    public class ProdutoDAO
    {
        private static readonly string erroDeConexao = "Não foi possível se conectar a nenhum banco de dados MySql especificado.";
            
        private static readonly string connectionString = "server=localhost;database=gerenciadorDeEstoque;uid=root;";

        public static List<Produto> ListarProdutos()
        {
            try
            {
                List<Produto> produtos = new List<Produto>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM produtos", connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto(
                                reader.GetString("nome"),
                                reader.GetInt32("quantidade"),
                                reader.GetDecimal("preco"));

                            produtos.Add(produto);
                        }
                    }
                }
                return produtos;
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
        
        public static List<Produto> ListarProdutosComFiltro(FiltroBase<Produto> filtro)
        {
            try
            {
                List<Produto> produtos = new List<Produto>();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM produtos", connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto(
                                reader.GetString("nome"),
                                reader.GetInt32("quantidade"),
                                reader.GetDecimal("preco"));

                            produtos.Add(produto);
                        }
                    }
                }

                return filtro.Aplicar(produtos.AsQueryable()).ToList();
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static void AdicionarProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("INSERT INTO produtos (nome, quantidade, preco) VALUES (@nome, @quantidade, @preco)", connection);
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@preco", produto.Preco);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Produto adicionado com sucesso!", "Adicionar Produto");
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }

        public static void EditarProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE produtos SET nome = @nome, quantidade = @quantidade, preco = @preco WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", produto.Id);
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@preco", produto.Preco);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Produto editado com sucesso!", "Operação realizada com sucesso",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }

        public static Produto BuscarProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM produtos WHERE nome = @nome AND quantidade = @quantidade AND preco = @preco", connection);
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                    command.Parameters.AddWithValue("@preco", produto.Preco);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produto.Id = reader.GetInt32("id");
                            produto.Nome = reader.GetString("nome");
                            produto.Quantidade = reader.GetInt32("quantidade");
                            produto.Preco = reader.GetDecimal("preco");
                        }
                    }
                    return produto;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static Produto BuscarProdutoPorNome(string nome)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM produtos WHERE nome = @nome", connection);
                    command.Parameters.AddWithValue("@nome", nome);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var produto = new Produto(
                                reader.GetString("nome"),
                                reader.GetInt32("quantidade"),
                                reader.GetDecimal("preco"));

                            return produto;
                        }
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static void RemoverProduto(Produto produto)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM produtos WHERE id = @id", connection);
                    command.Parameters.AddWithValue("@id", produto.Id);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Produto removido com sucesso!", "Operação realizada com sucesso",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao,
                    "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
