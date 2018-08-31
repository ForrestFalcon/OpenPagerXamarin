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

    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {

        const string TAG = "MyFirebaseMsgService";
        
        public override void OnMessageReceived(RemoteMessage message)
        {

        }


    }
}