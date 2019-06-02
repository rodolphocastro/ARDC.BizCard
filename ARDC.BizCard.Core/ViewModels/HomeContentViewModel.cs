using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    public class HomeContentViewModel : MvxNavigationViewModel
    {
        public HomeContentViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewCardViewModel>());
        }

        public IMvxAsyncCommand NavigateToCardCommand { get; private set; }
    }
}
