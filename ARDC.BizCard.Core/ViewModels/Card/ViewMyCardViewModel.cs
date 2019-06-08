using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Card
{
    public class ViewMyCardViewModel : MvxNavigationViewModel
    {
        public ViewMyCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;
            NavigateToEditMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<EditMyCardViewModel>());
        }

        private IBizCardService BizCardService { get; }

        public IMvxAsyncCommand NavigateToEditMyCardCommand { get; private set; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private byte[] _gravatarBytes;

        public byte[] GravatarBytes
        {
            get { return _gravatarBytes; }
            set { SetProperty(ref _gravatarBytes, value); }
        }


        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetMyCardAsync();
            if (!string.IsNullOrWhiteSpace(BizCard.Email))
                GravatarBytes = await BizCardService.GetGravatarAsync();
        }
    }
}
