using ARDC.BizCard.Core.ViewModels.Card;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    /// <summary>
    /// ViewModel para Landing do App, primeira tela de conteúdo do App.
    /// </summary>
    public class LandingViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância da LandingViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        public LandingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
        }

        /// <summary>
        /// Command para navegar á ViewModel de "Meu Cartão".
        /// </summary>
        public IMvxAsyncCommand NavigateToCardCommand { get; private set; }
    }
}
