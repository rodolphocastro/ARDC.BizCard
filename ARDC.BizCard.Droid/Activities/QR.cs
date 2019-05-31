
using Android.App;
using Android.OS;
using ARDC.BizCard.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ARDC.BizCard.Droid.Activities
{
    [Activity(Label = "@string/action_qrcode", Theme = "@style/AppTheme.NoActionBar")]
    public class QR : MvxAppCompatActivity<QrCodeViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.qr_code);
        }
    }
}