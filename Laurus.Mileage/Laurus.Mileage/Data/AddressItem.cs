using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Laurus.Mileage.Data
{
   public class AddressItem
   {
      [PrimaryKey, AutoIncrement]
      public int Id { get; set; }

        public string Name { get; set;}

      public string Address { get; set; }
   }
}
