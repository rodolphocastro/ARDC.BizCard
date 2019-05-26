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
            NavigateToEditCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<EditCardViewModel>());
            NavigateToCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewCardViewModel>());
        }

        public IMvxAsyncCommand NavigateToCardCommand { get; private set; }

        public IMvxAsyncCommand NavigateToEditCommand { get; private set; }
    }
}
