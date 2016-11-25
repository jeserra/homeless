using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Homeless
{
    public class HomelessApp:Application
    {
        public static double ScreenHeight;
        public static double ScreenWidth;

        public HomelessApp()
        {
            MainPage = new RootPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
