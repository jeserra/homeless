using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Homeless.Views;
using Homeless.Extras;
using Java.IO;
using Android.Graphics;
using Homeless.Droid.Utils;
 

namespace Homeless.Droid
{
     
    public static class PhotoApp
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }


    [Activity (Label = "Homeless.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        private Xamarin.Forms.Application HomelessApp;

        public string FileName { get; set; }
        

        public MainActivity()
        {
            

            PhotoApp._dir = new File(
                     Android.OS.Environment.GetExternalStoragePublicDirectory(
            Android.OS.Environment.DirectoryPictures), "Homeless");
            if (!PhotoApp._dir.Exists())
            {
                PhotoApp._dir.Mkdirs();
            }
        }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            Forms.Init(this, bundle);
            HomelessApp = new HomelessApp();

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            Homeless.HomelessApp.ScreenWidth = (width - 0.5f) / density;
            Homeless.HomelessApp.ScreenHeight = (height - 0.5f) / density;


            LoadApplication(HomelessApp);   
		}

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);


            PhotoApp.bitmap = PhotoApp._file.Path.LoadAndResizeBitmap((int)Homeless.HomelessApp.ScreenWidth, (int)Homeless.HomelessApp.ScreenHeight);

            System.IO.FileStream outstr = null;
            string pathNuevo = string.Empty;
            string data1 = string.Empty;

            try
            {
                // outstr = new FileOutputStream(PhotoApp._dir + @"\salida\imagen.png");
                pathNuevo =  String.Format(@"{0}\image{1}.png",  PhotoApp._dir , Guid.NewGuid().ToString());
                outstr = new System.IO.FileStream(pathNuevo, System.IO.FileMode.CreateNew);
               
                PhotoApp.bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, outstr);
                TextRecognition recognition = new TextRecognition();
                data1  = await recognition.GetText(outstr);
                
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("{0}", ex.Message);
            }
            finally
            {
                try
                {
                    if(outstr != null)
                    {
                        outstr.Close();
                    }
                }
                catch(IOException e)
                {
                    e.PrintStackTrace();
                }
            }

           // Aqui revisar el casteo de la pagina actual para ejecutar los onactivityresult
           var mp = ((NavigationPage) ((RootPage) HomelessApp.MainPage).Detail).CurrentPage;
            if (mp.GetType() == typeof(CaptureInStreetView))
            {
                MessagingCenter.Send<CaptureInStreetView, string>((CaptureInStreetView)mp, "foto capturada", pathNuevo);

                MessagingCenter.Send<CaptureInStreetView, string>((CaptureInStreetView)mp, "texto foto", data1);
            }

            GC.Collect();
        }

        public override void OnSaveInstanceState(Bundle outState, PersistableBundle outPersistentState)
        {
            base.OnSaveInstanceState(outState, outPersistentState);
            outState.PutString("filename", FileName);

        }


    }
}


