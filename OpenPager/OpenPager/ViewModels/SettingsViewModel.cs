using System.Collections.Generic;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using OpenPager.Helpers;
using Plugin.FirebasePushNotification;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenPager.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Properties

        string _fcmKey = string.Empty;

        public string FcmKey
        {
            get => _fcmKey;
            set => SetProperty(ref _fcmKey, value);
        }

        public bool PreferenceIsActive
        {
            get => Preferences.Get(Constants.PreferenceAlarmActivate, Constants.PreferenceAlarmActivateDefault);
            set => Preferences.Set(Constants.PreferenceAlarmActivate, value);
        }

        public bool PreferenceVibrate
        {
            get => Preferences.Get(Constants.PreferenceVibrate, Constants.PreferenceVibrateDefault);
            set => Preferences.Set(Constants.PreferenceVibrate, value);
        }

        public bool PreferenceTts
        {
            get => Preferences.Get(Constants.PreferenceTts, Constants.PreferenceTtsDefault);
            set => Preferences.Set(Constants.PreferenceTts, value);
        }

        public SettingsVolume PreferenceTtsVolume
        {
            get => new SettingsVolume(Preferences.Get(Constants.PreferenceTtsVolume, Constants.PreferenceTtsVolumeDefault));
            set => Preferences.Set(Constants.PreferenceTtsVolume, value.Volume);
        }

        public bool IsPreferenceAppCenterVisible => AppCenter.Configured;

        public bool PreferenceAppCenterCrash
        {
            get => Preferences.Get(Constants.PreferenceAppCenterCrash, Constants.PreferenceAppCenterDefault);
            set
            {
                Preferences.Set(Constants.PreferenceAppCenterCrash, value);
                Crashes.SetEnabledAsync(value);
            }
        }

        public bool PreferenceAppCenterAnalytics
        {
            get => Preferences.Get(Constants.PreferenceAppCenterAnalytics, Constants.PreferenceAppCenterDefault);
            set
            {
                Preferences.Set(Constants.PreferenceAppCenterAnalytics, value);
                Analytics.SetEnabledAsync(value);
            }
        }


        public List<SettingsVolume> PreferenceTtsVolumeList => new List<SettingsVolume>()
        {
            new SettingsVolume(0.25f),
            new SettingsVolume(0.5f),
            new SettingsVolume(0.75f),
            new SettingsVolume(1),
        };

        public Command ShareKeyCommand { get; set; }
        public Command ProfilerCommand { get; set; }

        #endregion

        public SettingsViewModel()
        {
            Title = "Einstellungen";
            FcmKey = CrossFirebasePushNotification.Current.Token; // DependencyService.Get<IPushHandler>().GetKey();

            ShareKeyCommand = new Command(ShareKey);
            ProfilerCommand = new Command(async () => { await Application.Current.MainPage.DisplayAlert("Profiler result", Profiler.Result(), "OK"); });
        }

        private async void ShareKey()
        {
            await DataTransfer.RequestAsync(new ShareTextRequest
            {
                Title = "Teile FCM-Key",
                Subject = "OpenPager FCM-Key",
                Text = FcmKey
            });
        }

        public class SettingsVolume
        {
            public float Volume { get; }

            public SettingsVolume(float volume)
            {
                this.Volume = volume;
            }

            public override string ToString()
            {
                return Volume * 100 + " %";
            }
        }
    }
}