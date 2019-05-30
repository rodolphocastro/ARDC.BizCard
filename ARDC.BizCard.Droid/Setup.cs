using Android.Support.Design.Button;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Widget;
using ARDC.BizCard.Core;
using MvvmCross.Droid.Support.V7.AppCompat;
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
        }
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(NestedScrollView).Assembly,
            typeof(TextInputEditText).Assembly,
            typeof(MaterialButton).Assembly
        };
    }
}