using System;

using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Widget;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;
using OpenPager.Models;

namespace OpenPager.Droid
{
    [Activity(Label = "OpenPager", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly Lazy<App> _app = new Lazy<App>();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CheckPlayService();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(_app.Value);
            
            CheckOperationJson(Intent);
        }

        protected override void OnNewIntent(Android.Content.Intent intent)
        {
            base.OnNewIntent(intent);
            CheckOperationJson(intent);
        }

        private void CheckOperationJson(Android.Content.Intent intent)
        {
            if (!intent.HasExtra(MyFirebaseMessagingService.INTENT_EXTRA_OPERATION))
            {
                return;
            }

            var operationJson = intent.GetStringExtra(MyFirebaseMessagingService.INTENT_EXTRA_OPERATION);
            if (String.IsNullOrEmpty(operationJson))
            {
                return;
            }

            var operation = JsonConvert.DeserializeObject<Operation>(operationJson);
            _app.Value.PushOperationAsync(operation);
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

