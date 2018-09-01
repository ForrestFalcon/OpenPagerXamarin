using System;
using Xamarin.Essentials;

namespace OpenPager.Helpers
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
