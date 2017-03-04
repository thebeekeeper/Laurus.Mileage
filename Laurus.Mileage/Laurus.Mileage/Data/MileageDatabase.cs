using Laurus.Mileage.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Laurus.Mileage.Data
{
   public class MileageDatabase
   {
      public MileageDatabase(string dbPath)
      {
         _database = new SQLiteAsyncConnection(dbPath);
         _database.CreateTableAsync<MileageItem>().Wait();
         _database.CreateTableAsync<AddressItem>().Wait();
      }

      public Task<List<T>> GetItemsAsync<T>() where T : new()
      {
         return _database.Table<T>().ToListAsync();
      }

      public Task<MileageItem> GetItemAsync(int id)
      {
         return _database.Table<MileageItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
      }

      public async void DeleteAllMileage()
      {
         var items = await _database.Table<MileageItem>().ToListAsync();
         foreach (var i in items)
            await _database.DeleteAsync(i);
      }

      public Task<int> SaveItemAsync(MileageItem item)
      {
         if (item.Id != 0)
         {
            return _database.UpdateAsync(item);
         }
         else
         {
            return _database.InsertAsync(item);
         }
      }

      public Task<int> SaveItemAsync(AddressItem item)
      {
         if (item.Id != 0)
         {
            return _database.UpdateAsync(item);
         }
         else
         {
            return _database.InsertAsync(item);
         }
      }

      public Task<int> DeleteItemAsync(MileageItem item)
      {
         return _database.DeleteAsync(item);
      }

      private readonly SQLiteAsyncConnection _database;
   }
}
