using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Homeless.Droid.Services;
using Homeless.DependencyServices;
using Xamarin.Forms;
using SQLite;


[assembly: Dependency(typeof(SQLiteAndroid))]
namespace Homeless.Droid.Services
{
    
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            string sqliteFileName = "Houses.mdb";
            string docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(docPath, sqliteFileName);
            SQLiteConnection conn = new SQLiteConnection(path);
            return conn;
        }
    }
     
}