using ARDC.BizCard.Core.ViewModels.Agenda;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
            NavigateToQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeViewModel>());
            NavigateToReadQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
            NavigateToAgendaCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AgendaViewModel>());
        }

        public IMvxAsyncCommand NavigateToMyCardCommand { get; private set; }

        public IMvxAsyncCommand NavigateToQrCommand { get; private set; }

        public IMvxAsyncCommand NavigateToReadQrCommand { get; private set; }

        public IMvxAsyncCommand NavigateToAgendaCommand { get; private set; }
    }
}
