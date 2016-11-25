using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
namespace Homeless.Controls
{
    public class HouseMap:Map   
    {
        public HouseMap(): base()
        {
            HousePins = new List<HousePin>();
        }
        public List<HousePin> HousePins { get; set; }
    }
}
