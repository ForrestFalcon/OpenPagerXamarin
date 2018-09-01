using System;
using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Media;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using OpenPager.Models;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Plugin.Permissions;
using Xamarin.Forms;

namespace OpenPager.Droid
{
    [Activity(Label = "OpenPager", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly Lazy<App> _app = new Lazy<App>();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);
            
            CheckPlayService();

            Forms.SetFlags("FastRenderers_Experimental");

            Profiler.Start("Forms.Init");
            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Profiler.Stop("Forms.Init");
            
            Profiler.Start("LoadApplication");
            LoadApplication(_app.Value);
            Profiler.Stop("LoadApplication");
            
            if (!CheckOperationJson(Intent))
            {
                FirebasePushNotificationManager.ProcessIntent(this, Intent);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Profiler.Stop("OnResume");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            if (!CheckOperationJson(intent))
            {
                FirebasePushNotificationManager.ProcessIntent(this, intent);
            }
        }

        private bool CheckOperationJson(Android.Content.Intent intent)
        {
            if (!intent.HasExtra(MainApplication.INTENT_EXTRA_OPERATION))
            {
                return false;
            }

            var operationJson = intent.GetStringExtra(MainApplication.INTENT_EXTRA_OPERATION);
            if (String.IsNullOrEmpty(operationJson))
            {
                return false;
            }

            AddAlarmFlags();

            var operation = JsonConvert.DeserializeObject<Operation>(operationJson);
            _app.Value.PushOperationAsync(operation);

            return true;
        }

        private void AddAlarmFlags()
        {
            Window.AddFlags(WindowManagerFlags.DismissKeyguard |
                            WindowManagerFlags.ShowWhenLocked |
                            WindowManagerFlags.TurnScreenOn |
                            WindowManagerFlags.AllowLockWhileScreenOn);
        }

        private void CheckPlayService()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device is not supported", ToastLength.Long);
                }
            }
        }
    }
}