using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.Mileage.Services
{
   public interface IFileHelper
   {
      string GetLocalFilePath(string filename);

      string Save(string filename, Stream stream);

      Stream ReadTemplate();

      Stream ReadFileContents(string fullPath);
   }
}
