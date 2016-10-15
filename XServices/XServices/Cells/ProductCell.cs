using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XServices.Cells
{
    public class ProductCell : ViewCell
    {
        public ProductCell()
        {
            var descriptionLabel = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
            };

            descriptionLabel.SetBinding(Label.TextProperty, new Binding("Description"));

            var priceLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.End,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
            };

            priceLabel.SetBinding(Label.TextProperty, new Binding("Price", stringFormat: "{0:C2}"));

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = {
                descriptionLabel, priceLabel,
            },
            };
        }
    }

}
