using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using ARDC.BizCard.Droid.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ARDC.BizCard.Droid.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            if (savedInstanceState == null)
            {
                LandingView landingFragment = new LandingView();
                var fTrans = SupportFragmentManager.BeginTransaction();
                fTrans.Add(Resource.Id.content_frame, landingFragment);
                fTrans.Commit();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case (Resource.Id.action_my_card):
                    ViewModel.NavigateToMyCardCommand.Execute();
                    break;
                case (Resource.Id.action_qrcode):
                    ViewModel.NavigateToQrCommand.Execute();
                    break;
                case (Resource.Id.action_read_card):
                    ViewModel.NavigateToReadQrCommand.Execute();
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

