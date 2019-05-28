using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels
{
    public class ViewCardViewModel : BaseViewModel
    {
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;

            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
        }

        private IBizCardService BizCardService { get; }

        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetCardAsync();
        }
    }
}
