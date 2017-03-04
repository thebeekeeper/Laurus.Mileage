using System;
using System.Collections.Generic;
using Laurus.Mileage.Data;
using Xamarin.Forms;

namespace Laurus.Mileage
{
    public partial class EditAddressView : ContentPage
    {
        private string _name;
        public string Name
        {
            get { return _name;}
            set { _name = value; OnPropertyChanged(nameof(Name));}
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(Address)); }
        }

        public EditAddressView() : this(new AddressItem() { Id = 0 })
        {
            
        }

        public EditAddressView(AddressItem item)
        {
            InitializeComponent();
            this.BindingContext = this;
            _item = item;
            if (item.Id == 0)
                this.Title = "New";
            else
                this.Title = "Edit";

            this.Name = item.Name;
            this.Address = item.Address;
        }

        void SaveClicked(object sender, EventArgs e)
        {
            App.Database.SaveItemAsync(new AddressItem()
            {
                Id = _item.Id,
                Name = this.Name,
                Address = this.Address,
            });
            Navigation.PopAsync();
        }

        private AddressItem _item;
    }
}
