using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OpenPager.Models;
using OpenPager.Views;
using OpenPager.ViewModels;

namespace OpenPager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationsPage : ContentPage
    {
        OperationsViewModel viewModel;

        public OperationsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new OperationsViewModel();

        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Operation;
            if (item == null)
                return;

            await Navigation.PushAsync(new OperationDetailPage(new OperationDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage(), true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async Task AboutUs_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }

        async Task Delete_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem) sender);
            await viewModel.DeleteOperation(mi.CommandParameter as Operation);
        }
    }
}