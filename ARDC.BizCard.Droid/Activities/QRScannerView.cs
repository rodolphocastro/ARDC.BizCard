using Android.App;
using Android.OS;
using ARDC.BizCard.Core.ViewModels.QR;
using Firebase.Analytics;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using ZXing;
using ZXing.Mobile;

namespace ARDC.BizCard.Droid.Activities
{
    /// <summary>
    /// Activity para a leitura de QR Codes.
    /// </summary>
    [Activity(Label = "@string/action_read_card", Theme = "@style/AppTheme.NoActionBar", NoHistory = true)]
    public class QRScannerView : MvxAppCompatActivity<QrCodeScannerViewModel>
    {
        /// <summary>
        /// Scanner para leitura dos QR Codes.
        /// </summary>
        public MobileBarcodeScanner Scanner { get; private set; }

        /// <summary>
        /// Rotina para criação da Activity.
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.qr_scanner);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FirebaseAnalytics.GetInstance(this).SetCurrentScreen(this, "QR Scanner", nameof(QRScannerView));

            MobileBarcodeScanner.Initialize(Application);
            Scanner = new MobileBarcodeScanner();

            ScanAsync();
        }

        /// <summary>
        /// Inicia o processo de Escaneamento através da Câmera.
        /// </summary>
        private async void ScanAsync()
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

        /// <summary>
        /// Processa o resultado do processo de Escaneamento.
        /// </summary>
        /// <param name="result">O resultado a ser processado</param>
        private void HandleScanResult(ZXing.Result result)
        {
            if (result != null && !string.IsNullOrEmpty(result.Text))
            {
                Scanner.Cancel();
                ViewModel.ReadCardCommand.Execute(result.Text);
                Finish();
            }
            else
            {
                ScanAsync();
            }
        }
    }
}