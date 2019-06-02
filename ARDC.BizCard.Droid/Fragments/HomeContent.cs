using Android.OS;
using Android.Runtime;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ARDC.BizCard.Droid.Fragments
{
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(LandingViewModel), AddToBackStack = true, FragmentContentId = Resource.Id.content_frame)]
    [Register(nameof(HomeContent))]
    public class HomeContent : MvxFragment<HomeContentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var _ = base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.content_main, null);
        }

        public override void OnResume()
        {
            base.OnResume();

            Activity?.SetTitle(Resource.String.app_name);
        }
    }
}