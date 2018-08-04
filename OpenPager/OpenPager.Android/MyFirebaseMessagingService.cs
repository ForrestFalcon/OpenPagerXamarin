using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Java.Lang;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using OpenPager.Models;
using Xamarin.Essentials;
using Double = System.Double;
using Exception = System.Exception;

namespace OpenPager.Droid
{

    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public const string INTENT_EXTRA_OPERATION = "operation";

        const string TAG = "MyFirebaseMsgService";
        
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Data: " + message.Data);

            bool isAlarmActive = Preferences.Get(Constants.PreferenceAlarmActivate, Constants.PreferenceAlarmActivateDefault);
            if (message.Data["type"].Equals("operation") && isAlarmActive)
            {
                Operation operation = mapDataToOperation(message.Data);

                Intent intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra(INTENT_EXTRA_OPERATION, JsonConvert.SerializeObject(operation));
                StartActivity(intent);
            }
        }

        private Operation mapDataToOperation(IDictionary<string, string> data)
        {
            Operation operation = new Operation();

            foreach (KeyValuePair<string, string> pair in data)
            {
                switch (pair.Key)
                {
                    case "key":
                        operation.Id = pair.Value;
                        break;
                    case "title":
                        operation.Title = pair.Value;
                        break;
                    case "message":
                        operation.Message = pair.Value;
                        break;
                    case "destination":
                        operation.Destination = pair.Value;
                        break;
                    case "destination_loc":
                        try
                        {
                            var value = pair.Value.Split(';');
                            operation.DestinationLat = float.Parse(value[0], CultureInfo.InvariantCulture.NumberFormat);
                            operation.DestinationLng = float.Parse(value[1], CultureInfo.InvariantCulture.NumberFormat);
                        }
                        catch (Exception e)
                        {
                            Log.Error(TAG, e.ToString());
                            Crashes.TrackError(e);
                        }
                        break;
                    case "timestamp":
                        try
                        {
                            operation.Time = UnixTimeStampToDateTime(long.Parse(pair.Value));
                        }
                        catch (Exception e)
                        {
                            Log.Error(TAG, e.ToString());
                            Crashes.TrackError(e);
                        }
                        break;
                }
            }

            return operation;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}