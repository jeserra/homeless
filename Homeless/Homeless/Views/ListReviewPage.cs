using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace Homeless.Views
{
    public class ListReviewPage:ContentPage
    {

        public ListView HousesListView { get; set; }
        public ListReviewPage()
        {
            Icon = "opportunities.png";
            Title = "List Houses";
            BackgroundColor = Color.FromHex("333333");

            HousesListView = new HousesListView();
            var menuLabel =
                new ContentView
                {
                    Padding = new Thickness(10, 36, 0, 5),
                    Content = new Label
                    {
                        TextColor = Color.FromHex("AAAAAA"),
                        Text = "List of Houses watched",
                    }
                };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand

            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(HousesListView);

            Content = layout;

            HousesListView.ItemSelected += HousesListView_ItemSelected;
        }

        private void HousesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new DetailsHousePage(((HouseMenuItem)e.SelectedItem).idHouse));
        }
    }
}
