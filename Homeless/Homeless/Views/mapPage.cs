using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Homeless.Models;
using Homeless.DependencyServices;
using Homeless.Extras;

namespace Homeless.Views
{
    public class mapPage:ContentPage
    {

        private databaseManager db = new databaseManager();

        public mapPage ()
        {
            InitializeComponents();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IGeoLocation>().activarGPS();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DependencyService.Get<IGeoLocation>().apagarGPS();
        }

        public void InitializeComponents ()
        {
            Dictionary<string, string> location = DependencyService.Get<IGeoLocation>().getLocation();

            Position pos = new Position(Convert.ToDouble(location["Lat"]), Convert.ToDouble(location["Lon"]));
            MapSpan span = MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(3));
            Map mapHouses = new Map(span);
            mapHouses.MapType = MapType.Street;
            mapHouses.IsShowingUser = false ;

            var listPins = getListPins();
           
            foreach (var item in listPins)
            {
                
                mapHouses.Pins.Add(item);

            }

            

            StackLayout layout = new StackLayout
            {
                 Orientation = StackOrientation.Vertical,
                 VerticalOptions = LayoutOptions.FillAndExpand,
                 HorizontalOptions = LayoutOptions.FillAndExpand
            };

            Title = "Mapa de propiedades";
            layout.Children.Add(mapHouses);
            Content = layout;
        }

        public List<Pin> getListPins()
        {
            List<House> houses = db.selectHouses();
            List<Pin> list = new List<Pin>();

            foreach(var item in houses)
            {
                double lat = Convert.ToDouble(item.Lat);
                double lon = Convert.ToDouble(item.Lon);
                Position pos = new Position(lat, lon);
                Pin pin = new Pin();
                pin.Position = pos;
                pin.Label = item.ShortDescription;
                pin.Type = PinType.SavedPin;
                list.Add(pin);
            }

            return list;
        }


    }
}
