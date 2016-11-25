using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Homeless.DependencyServices;
using Homeless.Droid.Services;
using Java.IO;
using Xamarin.Forms;
using Android.Provider;


[assembly: Xamarin.Forms.Dependency(typeof(CameraAndroid))]
namespace Homeless.Droid.Services
{
    public class CameraAndroid : ICamera
    {
        
        public static TaskCompletionSource<string> tcs;
        public Task<string> takePhoto()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            var activity = (Activity)Forms.Context;

            ((MainActivity)activity).FileName = string.Format("House_{0}.jpg", Guid.NewGuid());
            PhotoApp._file = new File(PhotoApp._dir, ((MainActivity)activity).FileName);
            var uri = Android.Net.Uri.FromFile(PhotoApp._file);
            intent.PutExtra(MediaStore.ExtraOutput, uri);
            activity.StartActivityForResult(intent, 0);
            tcs = new TaskCompletionSource<string>();
            return tcs.Task;
        }
    }
}