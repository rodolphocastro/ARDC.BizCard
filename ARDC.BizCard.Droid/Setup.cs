using Acr.UserDialogs;
using Android.Gms.Common;
using Android.Support.Design.Button;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using ARDC.BizCard.Core;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Droid.Services;
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
    /// <summary>
    /// Rotina de configuração do App.
    /// </summary>
    public class Setup : MvxAppCompatSetup<App>
    {
        /// <summary>
        /// Inicializa o App.
        /// </summary>
        /// <param name="pluginManager">Provedor de Plugins do MvvMCross</param>
        /// <param name="app">App a ser inicializado</param>
        protected override void InitializeApp(IMvxPluginManager pluginManager, IMvxApplication app)
        {
            base.InitializeApp(pluginManager, app);
            UserDialogs.Init(() => Mvx.IoCProvider.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
            Mvx.IoCProvider.RegisterSingleton<IAppLauncherService>(new AppLauncherService());
        }

        /// <summary>
        /// Assemblies a serem carregadas explicitamente, contrapondo a perda de performance da Reflexão.
        /// </summary>
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