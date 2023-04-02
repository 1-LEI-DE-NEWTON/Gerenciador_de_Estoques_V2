using Gerenciador_de_Estoques_V2.Domain.Models;
using GerenciadorDeEstoque.Filtros;
using Sistema_de_Gerenciamento_de_Estoques.Infra.DAO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gerenciador_de_Estoques_V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region LISTAS DOS ATRIBUTOS
        private readonly List<string> camposAdicionarProduto = new List<string> { "txtNomeProduto", "txtQuantidadeProduto", 
            "txtPrecoProduto","lblNomeProduto", "lblQuantidadeProduto", "lblPrecoProduto", "borderAddProduct", "txtAddProduct" };
        
        private readonly List<string> camposBemVindo = new List<string> { "borderWelcome", "txtWelcome", "txtWelcome2" };

        
        private readonly List<string> camposListarProduto = new List<string> { "lblFiltrar", "cbxFiltro", "btnFiltrar",
            "lvwProdutos", "btnDesejoFiltrar" };

        private readonly List<string> camposFiltrosListarProdutos = new List<string> { "txtFiltro", "txtMinimo", "txtMaximo" };

        #endregion
        
        public ObservableCollection<Produto> Produtos { get; set; }
        private Produto produtoSelecionado;
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        #region TELAS       
        
        private void TelaInicial()
        {
            SetVisibility(camposListarProduto, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Hidden);
            SetVisibility(camposFiltrosListarProdutos, Visibility.Hidden);
            SetVisibility(camposBemVindo, Visibility.Visible);
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var produto = ProdutoDAO.BuscarProdutoPorNome(txtPesquisarProdutoNome.Text);
            
            if (produto != null)
            {
                MessageBoxResult resultado = MessageBox.Show("Produto encontrado! Clique em Sim para editar ou Nao " +
                    "para visualizar todos os produtos com esse nome",
                    "Produto encontrado", MessageBoxButton.YesNoCancel);

                if (resultado == MessageBoxResult.Yes)
                {
                    EditarProdutos(produto);                    
                }
                
                else if (resultado == MessageBoxResult.No)
                {
                    ListarProdutosComBuscar(produto);                    
                }
            }
            
            else
            {
                MessageBox.Show("Produto não encontrado!", "Produto não encontrado",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }    

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            SetVisibility(camposAdicionarProduto, Visibility.Visible);
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposFiltrosListarProdutos, Visibility.Hidden);
            SetVisibility(camposListarProduto, Visibility.Hidden);
            btnSalvarProduto.Visibility = Visibility.Visible;
            btnSalvarProdutoEditado.Visibility = Visibility.Hidden;
        }

        private void ListarProdutos_Click(object sender, RoutedEventArgs e)
        {
            //torna todos os campos exceto o ListarProduto invisiveis
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Hidden);
            SetVisibility(camposListarProduto, Visibility.Visible);
            
            PreencherListView();
        }
        
        private void ListarProdutosComBuscar(Produto produto)
        {
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Hidden);
            SetVisibility(camposListarProduto, Visibility.Visible);
            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutosComFiltro(
                            new FiltroPorNome(produto.Nome)));
            lvwProdutos.ItemsSource = Produtos;
        }

        private void EditarProdutos(Produto produtoSelecionado)
        {
            //torna todos os campos exceto o EditarProduto invisiveis
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposFiltrosListarProdutos, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Visible);
            SetVisibility(camposListarProduto, Visibility.Hidden);
            btnSalvarProdutoEditado.Visibility = Visibility.Visible;
            btnSalvarProduto.Visibility = Visibility.Hidden;

            produtoSelecionado = ProdutoDAO.BuscarProduto(produtoSelecionado);
            txtNomeProduto.Tag = produtoSelecionado.Nome.ToString();            

            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutosComFiltro(
                new FiltroPorNome(produtoSelecionado.Nome)));
            lvwProdutos.ItemsSource = Produtos;
        }
        #endregion

        #region BOTOES DAS TELAS
        private void btnSalvarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomeProduto.Text != "" && txtQuantidadeProduto.Text != "" && txtPrecoProduto.Text != "")
            {
                if (ProdutoDAO.BuscarProdutoPorNome(txtNomeProduto.Text) == null)
                {
                    var produto = new Produto(txtNomeProduto.Text, int.Parse(txtQuantidadeProduto.Text),
                        (decimal)double.Parse(txtPrecoProduto.Text));
                    ProdutoDAO.AdicionarProduto(produto);                    
                    txtNomeProduto.Text = ""; txtQuantidadeProduto.Text = ""; txtPrecoProduto.Text = "";                                       
                }
                else
                {
                    MessageBox.Show("Já existe um produto com este nome", "Produto já existe!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "Preencha todos os campos",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        
        private void btnSalvarProdutoEditado_Click(object sender, RoutedEventArgs e)
        {

            if (txtNomeProduto.Text != "" && txtQuantidadeProduto.Text != "" && txtPrecoProduto.Text != "")
            {
                produtoSelecionado = ProdutoDAO.BuscarProduto(produtoSelecionado);
                produtoSelecionado.Nome = txtNomeProduto.Text;
                produtoSelecionado.Quantidade = int.Parse(txtQuantidadeProduto.Text);
                produtoSelecionado.Preco = (decimal)double.Parse(txtPrecoProduto.Text);

                
                ProdutoDAO.EditarProduto(produtoSelecionado);
                txtNomeProduto.Text = ""; txtQuantidadeProduto.Text = ""; txtPrecoProduto.Text = "";
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "Preencha todos os campos",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
                       
            TelaInicial();
        }

        private void EditarProduto_Click(object sender, RoutedEventArgs e)
        {

            if (lvwProdutos.SelectedItem != null)
            {
                produtoSelecionado = (Produto)lvwProdutos.SelectedItem;
                EditarProdutos(produtoSelecionado);
            }

            else
            {
                MessageBox.Show("Selecione um produto para editar!");
            }
        }

        private void ExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            if (lvwProdutos.SelectedItem != null)
            {
                produtoSelecionado = (Produto)lvwProdutos.SelectedItem;
                produtoSelecionado = ProdutoDAO.BuscarProduto(produtoSelecionado);

                MessageBoxResult resultado = MessageBox.Show("Deseja excluir o produto " + produtoSelecionado.Nome
                    + "?", "Excluir Produto", MessageBoxButton.YesNo);

                if (resultado == MessageBoxResult.Yes)
                {
                    ProdutoDAO.RemoverProduto(produtoSelecionado);
                    PreencherListView();
                }

                else
                {
                    MessageBox.Show("Operação cancelada!", "Excluir Produto");
                }
            }

            else
            {
                MessageBox.Show("Selecione um produto para remover!");
            }
        }

        private void DesejoFiltrar_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbxFiltroEscolhido = (ComboBoxItem)cbxFiltro.SelectedItem;
            if (cbxFiltroEscolhido != null)
            {
                string filtroEscolhido = cbxFiltroEscolhido.Content.ToString();

                switch (filtroEscolhido)
                {
                    case "Nome":
                        txtFiltro.Visibility = Visibility.Visible;
                        txtMinimo.Visibility = Visibility.Collapsed;
                        txtMaximo.Visibility = Visibility.Collapsed;

                        break;
                    case "Preço":
                        txtFiltro.Visibility = Visibility.Collapsed;
                        txtMinimo.Visibility = Visibility.Visible;
                        txtMaximo.Visibility = Visibility.Visible;
                        txtMinimo.Tag = "Preço Mínimo";
                        txtMaximo.Tag = "Preço Máximo";
                        break;
                    case "Quantidade":
                        txtFiltro.Visibility = Visibility.Collapsed;
                        txtMinimo.Visibility = Visibility.Visible;
                        txtMaximo.Visibility = Visibility.Visible;
                        txtMinimo.Tag = "Quantidade Mínima";
                        txtMaximo.Tag = "Quantidade Máxima";
                        break;
                }
            }

            else
            {
                MessageBox.Show("Selecione um Filtro!");
            }
            
        }

        private void Filtrar_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem cbxFiltroEscolhido = (ComboBoxItem)cbxFiltro.SelectedItem;
            string filtroEscolhido = cbxFiltroEscolhido.Content.ToString();

            if (txtFiltro.Text != "" || txtMaximo.Text != "" || txtMinimo.Text != "")
            {
                switch (filtroEscolhido)
                {
                    case "Preço":
                        if (decimal.TryParse(txtMinimo.Text, out decimal precoMinimo) &&
                            decimal.TryParse(txtMaximo.Text, out decimal precoMax))
                        {
                            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutosComFiltro(
                                new FiltroPreco(precoMinimo, precoMax)));
                            lvwProdutos.ItemsSource = Produtos;
                        }
                        
                        else
                        {
                            MessageBox.Show("Digite um valor válido!", "Filtro por Preço",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "Quantidade":
                        if (int.TryParse(txtMinimo.Text, out int qntMinima) &&
                            (int.TryParse(txtMaximo.Text, out int qntMax)))
                        {
                            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutosComFiltro(
                                new FiltroPreco(qntMinima, qntMax)));
                            lvwProdutos.ItemsSource = Produtos;
                        }
                        
                        else
                        {
                            MessageBox.Show("Digite um valor válido!", "Filtro por Quantidade",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;

                    case "Nome":
                        txtFiltro.Visibility = Visibility.Visible;
                        Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutosComFiltro(
                            new FiltroPorNome(txtFiltro.Text)));
                        lvwProdutos.ItemsSource = Produtos;
                        break;
                }
            }
            
            else
            {
                lvwProdutos.ItemsSource = Produtos;
            }
        }

        #endregion

        private void PreencherListView()
        {
            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutos());
            lvwProdutos.ItemsSource = Produtos;
        }
    
        private void SetVisibility(IEnumerable<string> campos, Visibility visibility)
        {
            foreach (string campo in campos)
            {
                FrameworkElement elemento = FindName(campo) as FrameworkElement;
                elemento?.Dispatcher.Invoke(() => elemento.Visibility = visibility);
            }
        }
    }
}
