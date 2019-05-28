
using Android.App;
using ARDC.BizCard.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ARDC.BizCard.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<MvxAppCompatSetup<App>, App>
    {
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
        }
    }
}