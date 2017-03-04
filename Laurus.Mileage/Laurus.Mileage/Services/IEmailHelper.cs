using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laurus.Mileage.Services
{
   public interface IEmailHelper
   {
      void SendEmail(string attachment);
   }
}
