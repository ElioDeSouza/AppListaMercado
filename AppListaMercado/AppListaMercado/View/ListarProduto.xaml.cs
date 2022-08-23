using AppListaMercado.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaMercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarProduto : ContentPage
    {
       
        ObservableCollection<Produtos> lista_produtos = new ObservableCollection<Produtos>();


        
        public ListarProduto()
        {
            InitializeComponent();

            lst_produtos.ItemsSource = lista_produtos;
        }


       
        private void ToolbarItem_Clicked_Novo(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new NovoProduto());

            }
            catch (Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }


       
        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            
            double soma = lista_produtos.Sum(i => i.Preco * i.Quantidade);

            string msg = "O total da compra é: " + soma;

            DisplayAlert("Ops", msg, "OK");
        }


        
        protected override void OnAppearing()
        {
            
            if (lista_produtos.Count == 0)
            {
                
                System.Threading.Tasks.Task.Run(async () =>
                {
                    
                    /*/List<Produtos> temp = await App.Database.GetAll();

                    foreach (Produtos item in temp)
                    {
                        lista_produtos.Add(item);
                    }*/

                    
                    ref_carregando.IsRefreshing = false;
                });
            }
        }


        
        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
           
            MenuItem disparador = (MenuItem)sender;


            
            Produtos produto_selecionado = (Produtos)disparador.BindingContext;

            bool confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");

            if (confirmacao)
            {
                
                await App.Database.Delete(produto_selecionado.Id);

                lista_produtos.Remove(produto_selecionado);
            }
        }


        
        private void txt_busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produtos> temp = await App.Database.Search(buscou);

                
                lista_produtos.Clear();

                foreach (Produtos item in temp)
                {
                    lista_produtos.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }


        
        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = (Produtos)e.SelectedItem
            });
        }
    }
}