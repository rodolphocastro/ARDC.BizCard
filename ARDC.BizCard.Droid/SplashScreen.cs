
using Android.App;
using ARDC.BizCard.Core;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ARDC.BizCard.Droid
{
    /// <summary>
    /// Splash Screen do Aplicativo.
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true, NoHistory = true)]
    public class SplashScreen : MvxSplashScreenAppCompatActivity<Setup, App>
    {
        /// <summary>
        /// Cria uma nova instância de SplashScreen.
        /// </summary>
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
        }
    }
}