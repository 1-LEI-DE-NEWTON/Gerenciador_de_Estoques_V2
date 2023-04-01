using Gerenciador_de_Estoques_V2.Domain.Models;
using Sistema_de_Gerenciamento_de_Estoques.Infra.DAO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Gerenciador_de_Estoques_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<string> camposAdicionarProduto = new List<string> { "txtNomeProduto", "txtQuantidadeProduto", 
            "txtPrecoProduto","lblNomeProduto", "lblQuantidadeProduto", "lblPrecoProduto", "borderAddProduct", "txtAddProduct" };
        
        private readonly List<string> camposBemVindo = new List<string> { "borderWelcome", "txtWelcome", "txtWelcome2" };

        //private Models models;                

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtPesquisarProdutoNome.Text;
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            foreach (string campo in camposBemVindo)
            {
                FrameworkElement elemento = FindName(campo) as FrameworkElement;
                if (elemento != null)
                {
                    elemento.Visibility = Visibility.Hidden;
                }
            }

            foreach (string campo in camposAdicionarProduto)
            {
                FrameworkElement elemento = FindName(campo) as FrameworkElement;
                if (elemento != null)
                {
                    elemento.Visibility = Visibility.Visible;
                }
            }                           
        }

        private void ListarProdutos_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtPesquisarProdutoNome.Text;
        }

        private void btnSalvarProduto_Click(object sender, RoutedEventArgs e)
        {            
            var produto = new Produto(txtNomeProduto.Text, int.Parse(txtQuantidadeProduto.Text),
                (decimal)double.Parse(txtPrecoProduto.Text));                        
            ProdutoDAO.AdicionarProduto(produto);            
            txtNomeProduto.Text = ""; txtQuantidadeProduto.Text = ""; txtPrecoProduto.Text = "";
        }

        private void Filtrar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtPesquisarProdutoNome.Text;
        }

        private void EditarProduto_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtPesquisarProdutoNome.Text;
        }
        
        private void ExcluirProduto_Click(object sender, RoutedEventArgs e)
        { }
    }
}
