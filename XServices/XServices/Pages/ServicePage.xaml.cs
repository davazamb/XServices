using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XServices.Cells;
using XServices.Classes;

namespace XServices.Pages
{
    public partial class ServicePage : ContentPage
    {
        private List<Product> products;

        public ServicePage()
        {
            InitializeComponent();
            LoadProducts();

            servicesListView.ItemTemplate = new DataTemplate(typeof(ServiceCell));
            servicesListView.RowHeight = 70;

            quantityStepper.ValueChanged += QuantityStepper_ValueChanged;
            addButton.Clicked += AddButton_Clicked;
            servicesListView.ItemSelected += ServicesListView_ItemSelected;
        }

        private async void ServicesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditServicePage((Service)e.SelectedItem));
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (productPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "You must select a product", "Accept");
                return;
            }

            var service = new Service
            {
                DateService = dateDatePicker.Date,
                DateRegistered = DateTime.Today,
                Price = products[productPicker.SelectedIndex].Price,
                ProductId = products[productPicker.SelectedIndex].ProductId,
                Quantity = quantityStepper.Value,
            };

            using (var da = new DataAccess())
            {
                da.Insert(service);
                var services = da.GetList<Service>(true)
                                    .Where(s => s.DateRegistered.Year == DateTime.Today.Year &&
                                                s.DateRegistered.Month == DateTime.Today.Month &&
                                                s.DateRegistered.Day == DateTime.Today.Day)
                                    .OrderByDescending(s => s.DateService)
                                    .ToList();
                servicesListView.ItemsSource = services;
            }

            productPicker.SelectedIndex = -1;
            dateDatePicker.Date = DateTime.Now;
            quantityEntry.Text = "1";
            quantityStepper.Value = 1;

            await DisplayAlert("Message", "The record was added", "Accept");
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            dateDatePicker.Date = DateTime.Now;

            using (var da = new DataAccess())
            {
                var services = da.GetList<Service>(true)
                                    .Where(s => s.DateRegistered.Year == DateTime.Today.Year &&
                                                s.DateRegistered.Month == DateTime.Today.Month &&
                                                s.DateRegistered.Day == DateTime.Today.Day)
                                    .OrderByDescending(s => s.DateService)
                                    .ToList();
                servicesListView.ItemsSource = services;
            }
        }

        private void QuantityStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            quantityEntry.Text = quantityStepper.Value.ToString();
        }
    }
}
