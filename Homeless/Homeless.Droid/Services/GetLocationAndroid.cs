using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Homeless.DependencyServices;
using Homeless.Droid;
using Xamarin.Forms;
using Homeless.Droid.Services;

[assembly:Dependency(typeof(GetLocationAndroid))]
namespace Homeless.Droid.Services
{
    public class GetLocationAndroid : Java.Lang.Object, ILocationListener, IGeoLocation
    {
        private LocationManager locationManager;
        private Dictionary<String, String> loc;

        public void activarGPS()
        {
            try
            {
                Context cnt = Android.App.Application.Context;
                locationManager = cnt.GetSystemService(Context.LocationService) as LocationManager;
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, this);
                System.Diagnostics.Debug.WriteLine("Activando GPS");
                Criteria criteria = new Criteria();
                criteria.Accuracy = Accuracy.Fine;
                string provider = locationManager.GetBestProvider(criteria, true);
                Location location = locationManager.GetLastKnownLocation(provider);
                if (location != null)
                {
                    newLocation(location);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void apagarGPS()
        {
            if (locationManager != null)
            {
                try
                {
                    locationManager.RemoveUpdates(this);
                    System.Diagnostics.Debug.WriteLine("Desactivando GPS....");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        public Dictionary<string, string> getLocation()
        {
            return loc;
        }

        public void OnLocationChanged(Location location)
        {
            newLocation(location);
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new NotImplementedException();
            return;
        }

        private void newLocation(Location location)
        {
            loc = new Dictionary<string, string>();
            loc.Add("Lat", location.Latitude.ToString());
            loc.Add("Lon", location.Longitude.ToString());
            System.Diagnostics.Debug.WriteLine("Detectando(Lat" + loc["Lat"] + "Lon" + loc["Lon"] + ")");

        }
    }
}