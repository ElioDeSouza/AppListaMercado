using AppListaMercado.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaMercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NovoProduto : ContentPage
    {
        public NovoProduto()
        {
            InitializeComponent();
        }

       
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                Produtos p = new Produtos
                {
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco = Convert.ToDouble(txt_preco.Text),
                };


                
                await App.Database.Insert(p);


                
                await DisplayAlert("Sucesso!", "Produto Cadastrado", "OK");


                
                await Navigation.PushAsync(new ListarProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}