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
using Android.Gms.Vision;


[assembly: Xamarin.Forms.Dependency(typeof(GetNumberPhone))]
namespace Homeless.Droid.Services
{
    public class GetNumberPhone : IGetPhoneFromImage
    {
        public string GetPhone(string path)
        {
            return string.Empty;
        }
    }
}