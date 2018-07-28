using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using OpenPager.Models;

namespace OpenPager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Operation Operation { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Operation = new Operation
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Item name",
                Message = "This is an item description.",
                Time = DateTime.Now
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Operation);
            await Navigation.PopModalAsync();
        }
    }
}