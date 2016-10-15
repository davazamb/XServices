using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XServices.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();

            productsButton.Clicked += ProductsButton_Clicked;
            serviesButton.Clicked += ServiesButton_Clicked;
            queriesButton.Clicked += QueriesButton_Clicked;
        }

        private async void QueriesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QueriesPage());
        }

        private async void ServiesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ServicePage());
        }

        private async void ProductsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductsPage());
        }

    }
}
