using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laurus.Mileage.Data;
using Xamarin.Forms;

namespace Laurus.Mileage.Views
{
   public partial class AddressesView : ContentPage
   {
        public ObservableCollection<AddressItem> Addresses { get; set;}

      public AddressesView()
      {
         InitializeComponent();
            this.BindingContext = this;
            Addresses = new ObservableCollection<AddressItem>();
            this.AddressList.ItemTapped += (sender, e) => 
            {
                Navigation.PushAsync(new EditAddressView((AddressItem)e.Item));
            };
      }

        protected override void OnAppearing()
        {
            this.AddressList.ItemTemplate = new DataTemplate(typeof(TextCell));
            this.AddressList.ItemTemplate.SetBinding(TextCell.TextProperty, "Name");
            this.AddressList.ItemTemplate.SetBinding(TextCell.DetailProperty, "Address");

            var items = App.Database.GetItemsAsync<AddressItem>().Result;
            this.Addresses = new ObservableCollection<AddressItem>(items);
            this.AddressList.ItemsSource = this.Addresses;
        }

        void AddAddressClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditAddressView());
        }
   }
}
