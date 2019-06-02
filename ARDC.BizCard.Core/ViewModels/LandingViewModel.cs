using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    public class LandingViewModel : MvxNavigationViewModel
    {
        public LandingViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToEditCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<EditMyCardViewModel>());
            NavigateToCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
            NavigateToQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeViewModel>());
            NavigateToReadQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
        }

        public IMvxAsyncCommand NavigateToCardCommand { get; private set; }

        public IMvxAsyncCommand NavigateToEditCommand { get; private set; }

        public IMvxAsyncCommand NavigateToQrCommand { get; private set; }

        public IMvxAsyncCommand NavigateToReadQrCommand { get; private set; }
    }
}
