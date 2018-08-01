using OpenPager.iOS.Services;
using OpenPager.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushHandler))]
namespace OpenPager.iOS.Services
{
    class PushHandler : IPushHandler
    {
        public string GetKey()
        {
            return "No Key";
        }
    }
}