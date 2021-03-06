﻿using System;
using Firebase.CloudMessaging;

using Foundation;
using Microsoft.AppCenter.Distribute;
using OpenPager.Helpers;
using Plugin.FirebasePushNotification;
using UIKit;
using Xamarin.Essentials;

namespace OpenPager.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Profiler.Start("Forms.Init");
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

            AiForms.Renderers.iOS.SettingsViewInit.Init();

            Distribute.DontCheckForUpdatesInDebug();
            Profiler.Stop("Forms.Init");

            Profiler.Start("LoadApplication");
            LoadApplication(new App());
            Profiler.Stop("LoadApplication");

            FirebasePushNotificationManager.Initialize(options, true);

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }

        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.

            // FirebasePushNotificationManager.DidReceiveMessage(userInfo); // this will call the onReceive method
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Do your magic to handle the notification data
            Console.WriteLine("DidReceiveRemoteNotification (State: {0}):", application.ApplicationState);
            Console.WriteLine(userInfo);

            if(application.ApplicationState == UIApplicationState.Background) {
                InvokeOnMainThread(AlarmHelper.Vibrate);
            }

            completionHandler(UIBackgroundFetchResult.NewData);
        }


    }
}
