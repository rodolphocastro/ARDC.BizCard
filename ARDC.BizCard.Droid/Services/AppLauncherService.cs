using Android.App;
using Android.Content;
using ARDC.BizCard.Core.Services;
using MvvmCross;
using MvvmCross.Platforms.Android;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Droid.Services
{
    public class AppLauncherService : IAppLauncherService
    {
        public readonly Activity _appActivity;

        public AppLauncherService()
        {
            _appActivity = Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
        }

        public Task LaunchEMailAsync(string mailAddress, CancellationToken ct)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("*/*");
            intent.PutExtra(Intent.ExtraEmail, mailAddress);

            _appActivity.StartActivity(intent);

            return Task.CompletedTask;
        }

        public Task LaunchLinkedInAsync(string profile, CancellationToken ct)
        {
            var uri = Android.Net.Uri.Parse($"https://linkedin.com/in/{profile}");
            var intent = new Intent(Intent.ActionView, uri);

            _appActivity.StartActivity(intent);

            return Task.CompletedTask;
        }

        public Task LaunchPhoneAsync(string phoneNumber, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task LaunchWebBrowserAsync(string url, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}