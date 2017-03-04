using Laurus.Mileage.Data;
using Laurus.Mileage.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Laurus.Mileage
{
   public partial class App : Application
   {
      public static MileageDatabase Database
      {
         get
         {
            if (_database == null)
            {
               _database = new MileageDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("mileage.db3"));
            }
            return _database;
         }
      }

      public App()
      {
         InitializeComponent();

         MainPage = new NavigationPage(new Laurus.Mileage.MainPage());
      }

      protected override void OnStart()
      {
         // Handle when your app starts
         var db = Database;
         var items = db.GetItemsAsync<MileageItem>().Result;
         if(items.Count() == 0)
         {
            db.SaveItemAsync(new MileageItem() { Time = DateTime.Now, StartOdometer = 20000, EndOdometer = 22000 }).Wait();
            db.SaveItemAsync(new MileageItem() { Time = DateTime.Now, StartOdometer = 22000, EndOdometer = 22001 }).Wait();
            db.SaveItemAsync(new MileageItem() { Time = DateTime.Now, StartOdometer = 23000, EndOdometer = 23005 }).Wait();
            db.SaveItemAsync(new MileageItem() { Time = DateTime.Now, StartOdometer = 25000, EndOdometer = 25009 }).Wait();
            db.SaveItemAsync(new MileageItem() { Time = DateTime.Now, StartOdometer = 28000, EndOdometer = 28010 }).Wait();
         }
         var addr = db.GetItemsAsync<AddressItem>().Result;
         if(addr.Count() == 0)
         {
            db.SaveItemAsync(new AddressItem() { Address = "123 main st." });
            db.SaveItemAsync(new AddressItem() { Address = "280 regency ct" });
         }
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }

      private static MileageDatabase _database;
   }
}
