using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using UIKit;

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
            var appcenterKey = System.Environment.GetEnvironmentVariable("APPCENTER_KEY");
            if (!String.IsNullOrEmpty(appcenterKey))
            {
                AppCenter.Start(appcenterKey, typeof(Analytics), typeof(Crashes));
            }
            
            Profiler.Start("Forms.Init");
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

            AiForms.Renderers.iOS.SettingsViewInit.Init();
            Profiler.Stop("Forms.Init");

            Profiler.Start("LoadApplication");
            LoadApplication(new App());
            Profiler.Stop("LoadApplication");
            
            return base.FinishedLaunching(app, options);
        }
    }
}
