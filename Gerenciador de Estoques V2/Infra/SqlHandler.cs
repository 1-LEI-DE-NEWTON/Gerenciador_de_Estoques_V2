using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Gerenciador_de_Estoques_V2.Infra
{
    public class SqlHandler
    {
        public static readonly string connectionString = "server=localhost;database=gerenciadorDeEstoque;uid=root;";
        
        private static readonly string erroDeConexao = "Não foi possível se conectar a nenhum banco de dados MySql especificado.";
        public static bool TestConnection()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(erroDeConexao, "Erro de conexão", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }        
    }
}
