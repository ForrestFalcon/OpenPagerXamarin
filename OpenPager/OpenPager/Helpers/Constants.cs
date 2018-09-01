using System;

namespace OpenPager.Helpers
{
    public class Constants
    {
        public const string MessageNewOperation = "NewOperation";

        public const string PreferenceAlarmActivate = "pref_alarm_activate";
        public const bool PreferenceAlarmActivateDefault = true;

        public const string PreferenceVibrate = "pref_vibrate";
        public const bool PreferenceVibrateDefault = true;

        public const string PreferenceTts = "pref_tts";
        public const bool PreferenceTtsDefault = true;

        public const string PreferenceTtsVolume = "pref_tts_volume";
        public const float PreferenceTtsVolumeDefault = 1;

        public static string AppCenterStart
        {
            get
            {
                string startup = string.Empty;

                if (Guid.TryParse(Secrets.AppCenter_iOS_Secret, out Guid iOSSecret))
                {
                    startup += $"ios={iOSSecret};";
                }

                if (Guid.TryParse(Secrets.AppCenter_Android_Secret, out Guid AndroidSecret))
                {
                    startup += $"android={AndroidSecret};";
                }

                return startup;
            }
        }
    }
}
