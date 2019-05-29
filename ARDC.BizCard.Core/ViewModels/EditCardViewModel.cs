using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels
{
    public class EditCardViewModel : MvxNavigationViewModel
    {
        public EditCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;

            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
            SaveChangesCommand = new MvxAsyncCommand(async () => await BizCardService.CreateOrEditCardAsync(BizCard));
        }
        private IBizCardService BizCardService { get; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }

        public IMvxAsyncCommand SaveChangesCommand { get; private set; }

        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetCardAsync();
        }
    }
}
