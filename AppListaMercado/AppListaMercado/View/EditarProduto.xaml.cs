using AppListaMercado.Model;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppListaMercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarProduto : ContentPage
    {
        public EditarProduto()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
               
                Produtos produto_anexado = BindingContext as Produtos;


                
                Produtos p = new Produtos
                {
                    //Id = ((Produtos) BindingContext).Id,
                    Id = produto_anexado.Id,
                    Descricao = txt_descricao.Text,
                    Quantidade = Convert.ToDouble(txt_quantidade.Text),
                    Preco = Convert.ToDouble(txt_preco.Text),
                };

               
                await App.Database.Update(p);

                await DisplayAlert("Sucesso!", "Produto Editado", "OK");

                await Navigation.PushAsync(new ListarProduto());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}