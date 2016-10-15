using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XServices.Classes;

namespace XServices.Pages
{
    public partial class EditServicePage : ContentPage
    {
        private List<Product> products;
        private Service service;

        public EditServicePage(Service service)
        {
            InitializeComponent();
            this.service = service;
            LoadProducts();
            LoadForm();

            quantityStepper.ValueChanged += QuantityStepper_ValueChanged;
            updateButton.Clicked += UpdateButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var rta = await DisplayAlert("Confirm", "Are you sure to delete the record?", "Yes", "No");
            if (!rta)
            {
                return;
            }

            using (var da = new DataAccess())
            {
                da.Delete(service);
            }

            await DisplayAlert("Message", "The record was deleted", "Acept");
            await Navigation.PopAsync();
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (productPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "You must select a product", "Accept");
                return;
            }

            service.DateService = dateDatePicker.Date;
            service.Price = products[productPicker.SelectedIndex].Price;
            service.ProductId = products[productPicker.SelectedIndex].ProductId;
            service.Quantity = quantityStepper.Value;

            using (var da = new DataAccess())
            {
                da.Update(service);
            }

            await DisplayAlert("Message", "The record was updated", "Acept");
            await Navigation.PopAsync();
        }

        private void LoadForm()
        {
            int i = 0;
            for (; i < products.Count; i++)
            {
                if (products[i].ProductId == service.ProductId)
                {
                    break;
                }
            }

            if (i == products.Count)
            {
                i = -1;
            }

            productPicker.SelectedIndex = i;
            quantityEntry.Text = service.Quantity.ToString();
            quantityStepper.Value = service.Quantity;
            dateDatePicker.Date = service.DateService;
        }

        private void QuantityStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            quantityEntry.Text = quantityStepper.Value.ToString();
        }

        private void LoadProducts()
        {
            using (var da = new DataAccess())
            {
                products = da.GetList<Product>(false).OrderBy(p => p.Description).ToList();
            }

            foreach (var product in products)
            {
                productPicker.Items.Add(product.Description);
            }
        }

    }
}
