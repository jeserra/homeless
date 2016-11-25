using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Homeless.Controls;
using Homeless.Models;
using Homeless.DependencyServices;
using Homeless.Extras;


namespace Homeless.Views
{
    public class mapCustomPage:ContentPage
    {

        private databaseManager db = new databaseManager();


        public mapCustomPage()
        {
            var houseMap = new HouseMap
            {
                 MapType = MapType.Street,
                 WidthRequest = Homeless.HomelessApp.ScreenWidth,
                 HeightRequest = Homeless.HomelessApp.ScreenHeight
            };

            Dictionary<string, string> location = DependencyService.Get<IGeoLocation>().getLocation();

            Position currentpos = new Position(Convert.ToDouble(location["Lat"]), Convert.ToDouble(location["Lon"]));
            //var pin = new HousePin
            //{
            //    Pin = new Pin
            //    {
            //        Type = PinType.Place,
            //        Position = new Position(37.79752, -122.40183),
            //        Label = "Tiempo development",
            //        Address = "Justo sierra algo, San Francisco CA"
            //    },
            //    Id = "Xamarin",
            //     url ="http://tiempodevelopment.com"
            //};
            //houseMap.HousePins = new List<HousePin> { pin };

            try
            {

                var listPins = getListPins();
                foreach (var item in listPins)
                {
                    houseMap.HousePins.Add(item);
                }
          
                houseMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    currentpos, Distance.FromKilometers(3.0)));
            }catch(Exception ex)
            {
                Title = ex.Message;
               // System.Console.WriteLine(ex.Message);
            }
            Content = houseMap;
        }

        public List<HousePin> getListPins()
        {
            List<House> houses = db.selectHouses();
            List<HousePin> list = new List<HousePin>();

            foreach (var item in houses)
            {
                HousePin housepin = new HousePin();

                double lat = Convert.ToDouble(item.Lat);
                double lon = Convert.ToDouble(item.Lon);
                Position pos = new Position(lat, lon);
                housepin.Pin = new Pin();
                housepin.Pin.Position = pos;
                housepin.Pin.Label = item.ShortDescription;
                housepin.Pin.Type = PinType.SavedPin;
                housepin.Id = item.IdHouse.ToString();
                housepin.url = "http://www.tiempodevelopment.com";

                list.Add(housepin);
            }

            return list;
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

    }
}
