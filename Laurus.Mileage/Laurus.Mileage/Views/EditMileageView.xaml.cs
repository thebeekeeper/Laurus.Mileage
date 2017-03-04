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

            _locations = App.Database.GetItemsAsync<AddressItem>().Result;

            foreach (var s in _locations)
            {
                this.StartLocationPicker.Items.Add(s.Name);
                this.EndLocationPicker.Items.Add(s.Name);
            }

            var start = _locations.FirstOrDefault(a => a.Id == item.StartId);
            var end = _locations.FirstOrDefault(a => a.Id == item.EndId);
            this.StartLocationPicker.SelectedIndex = _locations.IndexOf(start);
            this.EndLocationPicker.SelectedIndex = _locations.IndexOf(end);

            this.BindingContext = this;
            this.Date = item.Time;
            this.Start = item.StartOdometer;
            this.End = item.EndOdometer;
            _id = item.Id;
        }

        void SaveButtonClicked(object sender, EventArgs e)
        {
            var startId = -1;
            if(this.StartLocationPicker.SelectedIndex >= 0)
                startId = _locations.ElementAt(this.StartLocationPicker.SelectedIndex).Id;
            var endId = -1;
            if(this.EndLocationPicker.SelectedIndex >= 0)
                endId = _locations.ElementAt(this.EndLocationPicker.SelectedIndex).Id;

            App.Database.SaveItemAsync(new Data.MileageItem()
            {
                Id = _id,
                Time = Date,
                StartOdometer = Start,
                EndOdometer = End,
                StartId = startId,
                EndId = endId,
            });
            Navigation.PopAsync();
        }

        private int _id;
        private IList<AddressItem> _locations;
    }
}
