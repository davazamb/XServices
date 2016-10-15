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
    public partial class QueriesPage : ContentPage
    {
        public QueriesPage()
        {
            InitializeComponent();

            servicesListView.ItemTemplate = new DataTemplate(typeof(ServiceCell));
            servicesListView.RowHeight = 70;

            dateDatePicker.Date = DateTime.Today;
            LoadServices();

            dateDatePicker.DateSelected += DateDatePicker_DateSelected;
        }

        private void DateDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            LoadServices();
        }

        private void LoadServices()
        {
            using (var da = new DataAccess())
            {
                var list = da.GetList<Service>(true)
                                .Where(s => s.DateService.Year == dateDatePicker.Date.Year &&
                                            s.DateService.Month == dateDatePicker.Date.Month &&
                                            s.DateService.Day == dateDatePicker.Date.Day)
                            .ToList();
                var total = list.Sum(l => l.Value);
                servicesListView.ItemsSource = list;
                totalEntry.Text = string.Format("{0:C2}", total);
            }
        }

    }
}
