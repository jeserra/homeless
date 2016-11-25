using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Homeless.DependencyServices;
using Homeless.Models;

namespace Homeless.Extras
{
    public class databaseManager
    {
        private static int DATABASE_VERSION = 1;
        private SQLiteConnection db;
        private int oldVersion = 0;

        public databaseManager()
        {
            db = DependencyService.Get<ISQLite>().GetConnection();
            if (HomelessApp.Current.Properties.ContainsKey("DATABASE_VERSION"))
                oldVersion = (int)HomelessApp.Current.Properties["DATABASE_VERSION"];

            if (DATABASE_VERSION != oldVersion && oldVersion != 0)
                onUpgrade();
            if (oldVersion == 0)
                createTable();


        }

        public void createTable()
        {
            db.CreateTable<House>();
            Application.Current.Properties["DATABASE_VERSION"] = DATABASE_VERSION;
        }

        private void onUpgrade()
        {
            db.DropTable<House>();
            createTable();
        }

        public List<House> selectHouses ()
        {
            List<House> result = db.Query<House>(("Select * from [House]"));
            return result;
        }

        public House selectHouseById(int houseId)
        {
            string Query = String.Format("Select * from [House] where IdHouse = {0}", houseId);
            List<House> result = db.Query<House>(Query);
            return result.FirstOrDefault();
        }

        public int insertItem (House item)
        {
            int resul  = db.Insert(item);
            return resul;
        }

        public int updateItem (House item)
        {
            int result = db.Update(item);
            return result;
        }

        public int deleteItem (int HouseId)
        {
            int result = db.Delete<House>(HouseId);
            return result;
        }

        public void closeConnection()
        {
            db.Close();
        }
    }
}
