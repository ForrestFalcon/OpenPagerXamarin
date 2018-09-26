using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OpenPager.Models;
using OpenPager.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

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
        
        private async void CheckPermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Standort benötigt", "Für die Verwendung der Einsatzkarte wird der aktuelle Standort benötigt", "OK");

                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {

            }
            else if (status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            CheckPermissions();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        } 
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Operation;
            if (item == null)
                return;

            await Navigation.PushAsync(new OperationTabPage(item));

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