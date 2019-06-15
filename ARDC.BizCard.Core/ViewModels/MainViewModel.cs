using ARDC.BizCard.Core.ViewModels.Agenda;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    /// <summary>
    /// ViewModel principal, responsável pela navegação do Menu.
    /// </summary>
    public class MainViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância da MaiNViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de Logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de Navegação a ser utilizado</param>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            // TODO: Atualizar para que a navegação seja realizada através de uma MvxNotifyTask
            NavigateToMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
            NavigateToQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeViewModel>());
            NavigateToReadQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
            NavigateToAgendaCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AgendaViewModel>());
        }

        /// <summary>
        /// Command para navegar à ViewModel de "Meu Cartão".
        /// </summary>
        public IMvxAsyncCommand NavigateToMyCardCommand { get; private set; }

        /// <summary>
        /// Command para navegar à ViewModel de "Meu QR Code".
        /// </summary>
        public IMvxAsyncCommand NavigateToQrCommand { get; private set; }

        /// <summary>
        /// Command para navegar à ViewModel de "Ler QR Code".
        /// </summary>
        public IMvxAsyncCommand NavigateToReadQrCommand { get; private set; }   // TODO: Verificar para remover este Command.

        /// <summary>
        /// Command para navegar à ViewModel de "Agenda".
        /// </summary>
        public IMvxAsyncCommand NavigateToAgendaCommand { get; private set; }
    }
}
