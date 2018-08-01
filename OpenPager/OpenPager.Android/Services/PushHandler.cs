using Firebase.Iid;
using OpenPager.Droid.Services;
using OpenPager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushHandler))]
namespace OpenPager.Droid.Services
{
    class PushHandler : IPushHandler
    {
        public string GetKey()
        {
            return FirebaseInstanceId.Instance.Token;
        }
    }
}