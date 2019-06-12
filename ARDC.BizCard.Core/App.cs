using Acr.UserDialogs;
using ARDC.BizCard.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core
{
    /// <summary>
    /// Executável do Aplicativo.
    /// </summary>
    public class App : MvxApplication
    {
        /// <summary>
        /// Inicializa o aplicativo.
        /// </summary>
        public override void Initialize()
        {
            // Registrar todos os Tipos
            // Terminados com Service
            // Como interfances
            // De maneira LazySingleton
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            // Registrar Singleton da biblioteca UserDialogs
            Mvx.IoCProvider.RegisterSingleton(() => UserDialogs.Instance);

            // Indicar em qual ViewModel o app deve iniciar
            RegisterAppStart<LandingViewModel>();
        }
    }
}
