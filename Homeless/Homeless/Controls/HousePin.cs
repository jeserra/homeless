using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Homeless.Controls
{
    public class HousePin
    {
        public string Id { get; set; }
        public Pin Pin { get; set; }
        public string url { get; set; }
    }
}
