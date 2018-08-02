using OpenPager.Services;
using Xamarin.Essentials;
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
            ShareKeCommand = new Command(ShareKey);
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
    }
}