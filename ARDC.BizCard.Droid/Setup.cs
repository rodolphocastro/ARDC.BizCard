using Acr.UserDialogs;
using Android.Support.Design.Button;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using ARDC.BizCard.Core;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace ARDC.BizCard.Droid
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override void InitializeApp(IMvxPluginManager pluginManager, IMvxApplication app)
        {
            base.InitializeApp(pluginManager, app);
            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        }
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Android.Widget.Toolbar).Assembly,
            typeof(NestedScrollView).Assembly,
            typeof(TextInputEditText).Assembly,
            typeof(MaterialButton).Assembly,
            typeof(RecyclerView).Assembly,
            typeof(MvxRecyclerView).Assembly

        };
    }
}