using Android.OS;
using Android.Runtime;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using ARDC.BizCard.Core.ViewModels.QR;
using Firebase.Analytics;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ARDC.BizCard.Droid.Fragments
{
    /// <summary>
    /// Fragment para apresentação do "Meu QR Code".
    /// </summary>
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(MainViewModel), AddToBackStack = true, FragmentContentId = Resource.Id.content_frame)]
    [Register(nameof(QRView))]
    public class QRView : MvxFragment<QrCodeViewModel>
    {
        /// <summary>
        /// Rotina para a criação do Fragment.
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns>A View do fragment</returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var _ = base.OnCreateView(inflater, container, savedInstanceState);

            FirebaseAnalytics.GetInstance(Activity).SetCurrentScreen(Activity, "QR Code", nameof(QRView));

            return this.BindingInflate(Resource.Layout.qr_code, null);
        }

        /// <summary>
        /// Rotina para continuação do Fragment.
        /// </summary>
        public override void OnResume()
        {
            base.OnResume();

            Activity?.SetTitle(Resource.String.action_qrcode);
        }
    }
}