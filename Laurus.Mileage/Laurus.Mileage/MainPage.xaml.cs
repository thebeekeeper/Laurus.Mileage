using Laurus.Mileage.Data;
using Laurus.Mileage.Services;
using Laurus.Mileage.Views;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Laurus.Mileage
{
   public partial class MainPage : ContentPage
   {
      public ObservableCollection<MileageModel> MileageItems { get; set; }

      public MainPage()
      {
         InitializeComponent();
         this.Title = "Mileage Tracker";
         this.MileageEntries.ItemSelected += MileageEntries_ItemSelected;
         this.MileageEntries.ItemTapped += MileageEntries_ItemTapped;
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();

         var items = App.Database.GetItemsAsync<MileageItem>().Result;
         this.MileageEntries.ItemTemplate = new DataTemplate(typeof(TextCell));
         this.MileageEntries.ItemTemplate.SetBinding(TextCell.TextProperty, "Title");
         this.MileageEntries.ItemTemplate.SetBinding(TextCell.DetailProperty, "Time");

         this.MileageItems = new ObservableCollection<MileageModel>(items.Select(i => new MileageModel() { Id = i.Id, Title = string.Format("Trip {0}", i.Id), Time = i.Time.ToString("d") }));
         this.MileageEntries.ItemsSource = this.MileageItems;
      }

      private void MileageEntries_ItemTapped(object sender, ItemTappedEventArgs e)
      {
         var id = ((MileageModel)e.Item).Id;
         var item = App.Database.GetItemAsync(id).Result;
         Navigation.PushAsync(new EditMileageView(item));
      }

      private void MileageEntries_ItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
      }

      void AddButtonClicked(object sender, EventArgs e)
      {
         Navigation.PushAsync(new EditMileageView());
      }

      void AddAddressClicked(object sender, EventArgs e)
      {
         Navigation.PushAsync(new AddressesView());
      }

      async void ClearAllClicked(object sender, EventArgs e)
      {
         var answer = await DisplayAlert("Confirm", "Are you sure you want to delete all old entries?", "Yes", "No");
         if(answer)
         {
            App.Database.DeleteAllMileage();
            this.MileageItems.Clear();
         }
      }

      void ExportClicked(object sender, EventArgs e)
      {
         var f = DependencyService.Get<IFileHelper>().ReadTemplate();
         using(var excelEngine = new ExcelEngine())
         {

            //Set the default application version as Excel 2013.
            excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2013;

            //Create a workbook with a worksheet
            //IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);
            var workbook = excelEngine.Excel.Workbooks.Open(f);

            //Access first worksheet from the workbook instance.
            IWorksheet worksheet = workbook.Worksheets[0];

            //Enabling formula calculation.
            worksheet.EnableSheetCalculations();

            var items = App.Database.GetItemsAsync<MileageItem>().Result;
            int row = 19;
            foreach (var i in items)
            {
                    var startAdd = App.Database.GetItemsAsync<AddressItem>().Result.FirstOrDefault(x => x.Id == i.StartId);
                    var startStr = string.Empty;
                    if (startAdd != null)
                        startStr = startAdd.Address;
                    var endAdd = App.Database.GetItemsAsync<AddressItem>().Result.FirstOrDefault(x => x.Id == i.EndId);
                    var endStr = string.Empty;
                    if (endAdd != null)
                        endStr = endAdd.Address;
                    var addr = string.Format("{0} to {1}", startStr, endStr);
               var cell = string.Format("A{0}", row);
               worksheet[cell].Text = i.Time.ToString("d");
               cell = string.Format("C{0}", row);
               worksheet[cell].Text = addr;
               cell = string.Format("V{0}", row);
               worksheet[cell].Number = i.StartOdometer;
               cell = string.Format("W{0}", row);
               worksheet[cell].Number = i.EndOdometer;
               row++;
            }

            //Save the workbook to stream in xlsx format. 
            MemoryStream stream = new MemoryStream();
            workbook.SaveAs(stream);

            workbook.Close();

            f.Close();

            var file = DependencyService.Get<IFileHelper>().Save("test.xlsx", stream);
            var email = DependencyService.Get<IEmailHelper>();
            email.SendEmail(file);
         }
      }
   }

   public class MileageModel
   {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Time { get; set; }
   }
}
