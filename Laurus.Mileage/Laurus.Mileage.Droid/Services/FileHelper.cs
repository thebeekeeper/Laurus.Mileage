using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Laurus.Mileage.Droid.Services;

[assembly: Dependency(typeof(FileHelper))]
namespace Laurus.Mileage.Droid.Services
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string filename)
      {
         string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         return Path.Combine(path, filename);
      }

      public void Save(string filename, Stream stream)
      {
         throw new NotImplementedException();
      }

      public Stream ReadTemplate()
      {
         throw new NotImplementedException();
      }
   }
}