using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace OpenPager.Helper
{
    public class AlarmHelper
    {
        public static void Vibrate()
        {
            if (Preferences.Get(Constants.PreferenceVibrate, Constants.PreferenceVibrateDefault))
            {
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
        }
    }
}
