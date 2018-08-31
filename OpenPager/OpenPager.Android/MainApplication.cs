using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Newtonsoft.Json;
using OpenPager.Helper;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using Xamarin.Essentials;

namespace OpenPager.Droid
{
#if DEBUG
    [Application(Debuggable = true)]
#else
[Application(Debuggable = false)]
#endif
    public class MainApplication : Application
    {
        public const string INTENT_EXTRA_OPERATION = "json_operation";

        public MainApplication(IntPtr handle, JniHandleOwnership transer)
            : base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Profiler.Start("OnResume");
            CrossCurrentActivity.Current.Init(this);

            //Set the default notification channel for your app when running Android Oreo
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                //Change for your default notification channel id here
                FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

                //Change for your default notification channel name here
                FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
            }

            FirebasePushNotificationManager.Initialize(this, false);

            //Handle notification when app is closed here
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                if (Xamarin.Forms.Application.Current == null)
                {
                    bool isAlarmActive = Preferences.Get(Constants.PreferenceAlarmActivate, Constants.PreferenceAlarmActivateDefault);
                    var operation = OperationHelper.MapFirebaseToOperation(p.Data);
                    if (isAlarmActive && p.Data != null)
                    {
                        Intent intent = new Intent(this, typeof(MainActivity));
                        intent.PutExtra(INTENT_EXTRA_OPERATION, JsonConvert.SerializeObject(operation));
                        StartActivity(intent);
                    }
                }
            };
        }
    }
}