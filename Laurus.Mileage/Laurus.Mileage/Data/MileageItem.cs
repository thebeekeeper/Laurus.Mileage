using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Laurus.Mileage.Data
{
   public class MileageItem
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }

      public DateTime Time { get; set; }

      public int StartOdometer { get; set; }

      public int EndOdometer { get; set; }

        public int StartId { get; set;}
        public int EndId { get; set; }
   }
}
