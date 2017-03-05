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
using Laurus.Mileage.Services;
using System.IO;


[assembly: Dependency(typeof(FileHelper))]
namespace Laurus.Mileage.Droid.Services
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }

        public string Save(string filename, Stream stream)
        {
            //string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string docFolder = Forms.Context.ExternalCacheDir.AbsolutePath;
            var filePath = Path.Combine(docFolder, filename);
            FileStream fileStream = File.Open(filePath, FileMode.Create);
            stream.Position = 0;
            stream.CopyTo(fileStream);
            fileStream.Flush();
            fileStream.Close();
            return filePath;
        }

        public Stream ReadTemplate()
        {
            var filename = "template.xlsx";
            var memStream = new MemoryStream();
            using (StreamReader sr = new StreamReader(Forms.Context.Assets.Open(filename)))
            {
                sr.BaseStream.CopyTo(memStream);
            }
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
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