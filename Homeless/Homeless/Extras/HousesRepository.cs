using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homeless.Models;
using Homeless.DependencyServices;

namespace Homeless.Extras
{
    public class HousesRepository
    {
        private databaseManager DB = new databaseManager();

        public HousesRepository()
        {

        }

        public List<House> getAllHouses ()
        {
            return DB.selectHouses();
        }
    }
}
