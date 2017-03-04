using Laurus.Mileage.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Laurus.Mileage.Views
{
   public partial class EditMileageView : ContentPage, INotifyPropertyChanged
   {
      private DateTime _date;
      public DateTime Date
      {
         get { return _date; }
         set { _date = value; OnPropertyChanged(nameof(Date)); }
      }

      private int _start;
      public int Start
      {
         get { return _start; } 
         set { _start = value; OnPropertyChanged(nameof(Start)); }
      }

      private int _end;
      public int End
      {
         get { return _end; }
         set { _end = value; OnPropertyChanged(nameof(End)); }
      }

      // this is for making a new item
      public EditMileageView() : this(new MileageItem() { Time = DateTime.Now, Id = 0 })
      {
      }

      public EditMileageView(MileageItem item) 
      {
         InitializeComponent();
         if (item.Id == 0)
            this.Title = "New";
         else
            this.Title = "Edit";
         this.BindingContext = this;
         this.Date = item.Time;
         this.Start = item.StartOdometer;
         this.End = item.EndOdometer;
         _id = item.Id;
      }

      void SaveButtonClicked(object sender, EventArgs e)
      {
         App.Database.SaveItemAsync(new Data.MileageItem() { Id = _id, Time = Date, StartOdometer = Start, EndOdometer = End });
         Navigation.PopAsync();
      }

      private int _id;
   }
}
