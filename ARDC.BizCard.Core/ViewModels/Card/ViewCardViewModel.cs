using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Card
{
    public class ViewCardViewModel : MvxNavigationViewModel
    {
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService, IQrCodeService qrCodeService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;
            QrCodeService = qrCodeService;
            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
        }

        private IBizCardService BizCardService { get; }
        private IQrCodeService QrCodeService { get; }

        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private byte[] _bizCardByteArray;

        public byte[] BizCardByteArray
        {
            get { return _bizCardByteArray; }
            set { SetProperty(ref _bizCardByteArray, value); }
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetCardAsync();

            string jsonCard = await BizCardService.GetCardAsJSONAsync();

            if (!string.IsNullOrWhiteSpace(jsonCard))
                BizCardByteArray = await QrCodeService.CreateQRCode(jsonCard);
        }
    }
}
