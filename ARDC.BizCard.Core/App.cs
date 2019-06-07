using Acr.UserDialogs;
using ARDC.BizCard.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);

            RegisterAppStart<LandingViewModel>();
        }
    }
}
