﻿using Gerenciador_de_Estoques_V2.Domain.Models;
using GerenciadorDeEstoque.Filtros;
using Sistema_de_Gerenciamento_de_Estoques.Infra.DAO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #region LISTAS DOS ATRIBUTOS
        private readonly List<string> camposAdicionarProduto = new List<string> { "txtNomeProduto", "txtQuantidadeProduto", 
            "txtPrecoProduto","lblNomeProduto", "lblQuantidadeProduto", "lblPrecoProduto", "borderAddProduct", "txtAddProduct" };
        
        private readonly List<string> camposBemVindo = new List<string> { "borderWelcome", "txtWelcome", "txtWelcome2" };

        private readonly List<string> camposListarProduto = new List<string> { "lblFiltrar", "cbxFiltro", "btnFiltrar", "lvwProdutos" };

        #endregion
            //private Models models;                
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
            SetVisibility(camposBemVindo, Visibility.Visible);
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            //alterar visbilidade
            string nome = txtPesquisarProdutoNome.Text;
        }

        private void AdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            SetVisibility(camposAdicionarProduto, Visibility.Visible);
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposListarProduto, Visibility.Hidden);
            btnSalvarProduto.Visibility = Visibility.Visible;
        }

        private void ListarProdutos_Click(object sender, RoutedEventArgs e)
        {
            //torna todos os campos exceto o ListarProduto invisiveis
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Hidden);
            SetVisibility(camposListarProduto, Visibility.Visible);            
            
            PreencherListView();
        }

        private void EditarProdutos(Produto produtoSelecionado)
        {
            //torna todos os campos exceto o EditarProduto invisiveis
            SetVisibility(camposBemVindo, Visibility.Hidden);
            SetVisibility(camposAdicionarProduto, Visibility.Visible);
            SetVisibility(camposListarProduto, Visibility.Hidden);
            btnSalvarProdutoEditado.Visibility = Visibility.Visible;

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
            var produto = new Produto(txtNomeProduto.Text, int.Parse(txtQuantidadeProduto.Text),
                (decimal)double.Parse(txtPrecoProduto.Text));                        
            ProdutoDAO.AdicionarProduto(produto);            
            txtNomeProduto.Text = ""; txtQuantidadeProduto.Text = ""; txtPrecoProduto.Text = "";
        }
        
        private void btnSalvarProdutoEditado_Click(object sender, RoutedEventArgs e)
        {
            produtoSelecionado = ProdutoDAO.BuscarProduto(produtoSelecionado);
            produtoSelecionado.Nome = txtNomeProduto.Text;
            produtoSelecionado.Quantidade = int.Parse(txtQuantidadeProduto.Text);
            produtoSelecionado.Preco = (decimal)double.Parse(txtPrecoProduto.Text);
            //Salva as alterações no banco de dados
            ProdutoDAO.EditarProduto(produtoSelecionado);
            TelaInicial();
        }

        #endregion
        #region TELA LISTAR PRODUTOS
        private void PreencherListView()
        {
            Produtos = new ObservableCollection<Produto>(ProdutoDAO.ListarProdutos());
            lvwProdutos.ItemsSource = Produtos;
        }

        private void Filtrar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtPesquisarProdutoNome.Text;
        }

        private void EditarProduto_Click(object sender, RoutedEventArgs e)
        {
            
            if (lvwProdutos.SelectedItem != null)
            {
                produtoSelecionado = (Produto)lvwProdutos.SelectedItem;
                EditarProdutos(produtoSelecionado);
                
                PreencherListView();
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
        #endregion
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
