using Laurus.Mileage.Services;
using Laurus.Mileage.UWP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Laurus.Mileage.UWP.Services
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string filename)
      {
         return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
      }

      public Stream ReadTemplate()
      {
         throw new NotImplementedException();
      }

      public void Save(string filename, Stream stream)
      {
         throw new NotImplementedException();
      }
   }
}
