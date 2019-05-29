using ARDC.BizCard.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Akavache.Registrations.Start("ArdcBizCard");

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<LandingViewModel>();
        }
    }
}
