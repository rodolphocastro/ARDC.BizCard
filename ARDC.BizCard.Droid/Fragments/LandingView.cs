using Android.OS;
using Android.Runtime;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ARDC.BizCard.Droid.Fragments
{
    /// <summary>
    /// Fragment para o Landing do App.
    /// </summary>
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(MainViewModel), AddToBackStack = true, FragmentContentId = Resource.Id.content_frame)]
    [Register(nameof(LandingView))]
    public class LandingView : MvxFragment<LandingViewModel>
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

            return this.BindingInflate(Resource.Layout.landing, null);
        }

        /// <summary>
        /// Rotina para continuação do Fragment.
        /// </summary>
        public override void OnResume()
        {
            base.OnResume();

            Activity?.SetTitle(Resource.String.app_name);
        }
    }
}