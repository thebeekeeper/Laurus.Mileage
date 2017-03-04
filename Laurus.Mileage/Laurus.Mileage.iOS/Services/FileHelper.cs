using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Laurus.Mileage.Services;
using Xamarin.Forms;
using Laurus.Mileage.iOS.Services;
using Foundation;

[assembly: Dependency(typeof(FileHelper))]
namespace Laurus.Mileage.iOS.Services
{
   public class FileHelper : IFileHelper
   {
      public string GetLocalFilePath(string filename)
      {
         string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

         if (!Directory.Exists(libFolder))
         {
            Directory.CreateDirectory(libFolder);
         }

         return Path.Combine(libFolder, filename);
      }

      public Stream ReadTemplate()
      {
         var filename = "template.xlsx";
         var path = Path.Combine(NSBundle.MainBundle.BundlePath, filename);
         return File.Open(path, FileMode.Open, FileAccess.Read);
      }

      public string Save(string filename, Stream stream)
      {
         string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
         var filePath = Path.Combine(docFolder, filename);
         FileStream fileStream = File.Open(filePath, FileMode.Create);
         stream.Position = 0;
         stream.CopyTo(fileStream);
         fileStream.Flush();
         fileStream.Close();
         return filePath;
      }

      public Stream ReadFileContents(string fullPath)
      {
         if (File.Exists(fullPath))
         {
            return File.OpenRead(fullPath);
         }
         return null;
      }
   }
}