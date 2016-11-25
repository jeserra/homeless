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
using Homeless.DependencyServices;
using Homeless.Droid.Services;
using Xamarin.Forms;


[assembly: Dependency(typeof(PhoneCallAndroid))]
namespace Homeless.Droid.Services
{
    public class PhoneCallAndroid : IPhoneCall
    {
        public void MakeQuickCall(string PhoneNumber)
        {
            try
            { 
                var uri = Android.Net.Uri.Parse(string.Format("tel:{0}", PhoneNumber));
                var intent = new Intent(Intent.ActionCall, uri);
                Xamarin.Forms.Forms.Context.StartActivity(intent);
            }
            catch(Exception ex)
            {
                new AlertDialog.Builder(Android.App.Application.Context).SetPositiveButton("OK", (sender, args) =>
                {

                })
                .SetMessage(ex.ToString())
                .SetTitle("Android Exception")
                .Show();

            }

        }
    }
}