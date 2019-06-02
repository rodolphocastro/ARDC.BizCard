using Android.App;
using Android.OS;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Droid.Support.V7.AppCompat;
using ZXing.Mobile;

namespace ARDC.BizCard.Droid.Activities
{
    [Activity(Label = "@string/action_read_card", Theme = "@style/AppTheme.NoActionBar")]
    public class QRScannerView : MvxAppCompatActivity<QrCodeScannerViewModel>
    {
        public MobileBarcodeScanner Scanner { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.qr_scanner);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            MobileBarcodeScanner.Initialize(Application);
            Scanner = new MobileBarcodeScanner();

            StartScan();
        }

        private async void StartScan()
        {
            var result = await Scanner.Scan();

            if (result != null)
                HandleScanResult(result);
        }

        private void HandleScanResult(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                ViewModel?.SaveCardCommand.Execute(result.Text);
                Scanner.Cancel();
            }
            else
            {
                StartScan();
            }
        }
    }
}