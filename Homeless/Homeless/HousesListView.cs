using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Homeless.Extras;

namespace Homeless
{
    public class HousesListView:ListView
    {
        public HousesListView()
        {
            init();
        }

        public HousesListView(ListViewCachingStrategy strategy) : 
            base (strategy)
        {
            init();


        }

        private void init ()
        {
            List<HouseMenuItem> ListHousesItems = new List<HouseMenuItem>();
            var repository = new HousesRepository();
            var listHouses = repository.getAllHouses();
            foreach (var item in listHouses)
            {
                ListHousesItems.Add(new HouseMenuItem()
                {
                    ImagePath = "contacts.png", // item.Images,
                    ShortDescription = item.ShortDescription,
                    Lat = item.Lat,
                    Lon = item.Lon,
                    idHouse = item.IdHouse
                });
            }

            ItemsSource = ListHousesItems;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "ShortDescription");
            cell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");

            ItemTemplate = cell;
            SelectedItem = ListHousesItems.FirstOrDefault();
        }
    }
}
