using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XServices.Cells
{
    public class ServiceCell : ViewCell
    {
        public ServiceCell()
        {
            var dateLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
            };

            dateLabel.SetBinding(Label.TextProperty, new Binding("DateService", stringFormat: "{0:yyyy/MM/dd}"));

            var descriptionLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
            };

            descriptionLabel.SetBinding(Label.TextProperty, new Binding("Product.Description"));

            var quantityLabel = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            quantityLabel.SetBinding(Label.TextProperty, new Binding("Quantity", stringFormat: "Quantity: {0:N2}"));

            var valueLabel = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            valueLabel.SetBinding(Label.TextProperty, new Binding("Value", stringFormat: "Value: {0:N2}"));


            var line1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                dateLabel, descriptionLabel,
            },
            };

            var line2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                quantityLabel, valueLabel,
            },
            };

            View = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = {
                line1, line2,
            },
            };
        }
    }

}
