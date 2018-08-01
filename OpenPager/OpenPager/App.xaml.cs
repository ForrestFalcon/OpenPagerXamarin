using System;
using System.IO;
using System.Threading.Tasks;
using OpenPager.Models;
using OpenPager.Services;
using OpenPager.ViewModels;
using Xamarin.Forms;
using OpenPager.Views;
using SQLite;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace OpenPager
{
    public partial class App : Application
    {
        private static readonly Lazy<SQLiteAsyncConnection> DatabaseLazy = new Lazy<SQLiteAsyncConnection>(() =>
            new SQLiteAsyncConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "database.db3")));

        public static SQLiteAsyncConnection Database => DatabaseLazy.Value;

        public App()
        {
            DependencyService.Register<IDataStore<Operation>, OperationDataStore>();

            InitializeComponent();
            MainPage = new NavigationPage(new OperationsPage());
        }

        public async void PushOperationAsync(Operation operation)
        {
            await DependencyService.Get<IDataStore<Operation>>().AddItemAsync(operation);

            await MainPage.Navigation.PushModalAsync(
                new NavigationPage(new OperationTabPage(operation)));

            MessagingCenter.Send(this, Constants.MessageNewOperation);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}