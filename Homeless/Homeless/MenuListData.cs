using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Homeless.Views;

namespace Homeless
{
    public class MenuListData:List<MenuItem>
    {
        public MenuListData()
        {
           

            this.Add(new MenuItem()
            {
                Title ="Quick Capture",
                IconSource = "contacts.png",
                TargetType = typeof(CaptureInStreetView)
            });

            this.Add(new MenuItem()
            {
                Title = "List Properties",
                IconSource = "opportunities.png",
                TargetType = typeof(ListReviewPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Mapa",
                IconSource = "leads.png",
                TargetType = typeof(mapCustomPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Acerca de",
                IconSource = "leads.png",
                TargetType = typeof(aboutPage)
            });
        }
    }
}
