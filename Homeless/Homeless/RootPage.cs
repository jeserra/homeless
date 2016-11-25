using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Homeless.Views;

namespace Homeless
{
    public class RootPage : Xamarin.Forms.MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);
            Master = menuPage;
            Detail = new NavigationPage(new CaptureInStreetView());
        }

        void NavigateTo (MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            Detail = new NavigationPage(displayPage);
            IsPresented = false;
        }
    }
}
