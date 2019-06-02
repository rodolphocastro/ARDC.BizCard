using Android.App;
using Android.OS;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using ZXing;
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
            var options = new MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<BarcodeFormat>() { BarcodeFormat.QR_CODE }
            };

            Scanner.TopText = Resources.GetString(Resource.String.scan_top_text);
            Scanner.BottomText = Resources.GetString(Resource.String.scan_bottom_text);

            var result = await Scanner.Scan(options);

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