using ARDC.BizCard.Core.ViewModels;
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

            Akavache.Registrations.Start("ArdcBizCard");

            RegisterAppStart<LandingViewModel>();
        }
    }
}
