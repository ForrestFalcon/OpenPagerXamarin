using System;
using System.IO;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using OpenPager.Helpers;
using OpenPager.Models;
using OpenPager.Services;
using Xamarin.Forms;
using OpenPager.Views;
using Plugin.FirebasePushNotification;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Device = Xamarin.Forms.Device;

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
            SetUpAppCenter();

            DependencyService.Register<IDataStore<Operation>, OperationDataStore>();

            if (!DesignMode.IsDesignModeEnabled)
            {
                VersionTracking.Track();
            }

            InitializeComponent();

            MainPage = new NavigationPage(new OperationsPage());

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                bool isAlarmActive = Preferences.Get(Constants.PreferenceAlarmActivate, Constants.PreferenceAlarmActivateDefault);
                var operation = OperationHelper.MapFirebaseToOperation(p.Data);
                if (isAlarmActive && operation != null)
                {
                    PushOperationAsync(operation);
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                // Notification is opened (probably from iOS)
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }

                var operation = OperationHelper.MapFirebaseToOperation(p.Data);
                if (operation != null)
                {
                    PushOperationAsync(operation, false);
                }
            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }
                }
            };
        }

        private async void SetUpAppCenter()
        {
            AppCenter.Start(Constants.AppCenterStart, typeof(Analytics), typeof(Crashes), typeof(Distribute));

            var crash = Preferences.Get(Constants.PreferenceAppCenterCrash, Constants.PreferenceAppCenterDefault);
            await Crashes.SetEnabledAsync(crash);
            
            var analytics = Preferences.Get(Constants.PreferenceAppCenterAnalytics, Constants.PreferenceAppCenterDefault);
            await Analytics.SetEnabledAsync(analytics);
        }

        public void PushOperationAsync(Operation operation, bool isAlarm = true)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DependencyService.Get<IDataStore<Operation>>().AddItemAsync(operation);

                await MainPage.Navigation.PushAsync(new OperationTabPage(operation, true));

                MessagingCenter.Send(this, Constants.MessageNewOperation);
            });
        }

        protected override void OnStart()
        {
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