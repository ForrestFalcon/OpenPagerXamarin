using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OpenPager.Services;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace OpenPager.ViewModels
{
    public class SettingsViewModell : BaseViewModel
    {
        string _fcmKey = string.Empty;

        public string FcmKey
        {
            get => _fcmKey;
            set => SetProperty(ref _fcmKey, value);
        }

        public Command ShareKeCommand { get; set; }

        public SettingsViewModell()
        {
            Title = "Einstellungen";
            FcmKey = DependencyService.Get<IPushHandler>().GetKey();
            ShareKeCommand = new Command(() =>
            {
                if (!CrossShare.IsSupported)
                    return;

                CrossShare.Current.Share(new ShareMessage
                {
                    Title = "OpenPager FCM-Key",
                    Text = FcmKey
                });
            });
        }
    }
}