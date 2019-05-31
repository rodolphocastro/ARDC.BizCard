using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels
{
    public class QrCodeViewModel : MvxNavigationViewModel
    {
        public QrCodeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService, IQrCodeService qrCodeService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService ?? throw new ArgumentNullException(nameof(bizCardService));
            QrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<HomeContentViewModel>());
        }

        private IBizCardService BizCardService { get; }
        private IQrCodeService QrCodeService { get; }

        private byte[] _qrBytes;

        public byte[] QrBytes
        {
            get { return _qrBytes; }
            set { SetProperty(ref _qrBytes, value); }
        }

        private bool _hasQrData = false;

        public bool HasQrData
        {
            get { return _hasQrData; }
            set { SetProperty(ref _hasQrData, value); }
        }

        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }

        public override async Task Initialize()
        {
            await base.Initialize();

            string cardJson = await BizCardService.GetCardAsJSONAsync();

            if (!string.IsNullOrEmpty(cardJson))
                QrBytes = await QrCodeService.CreateQRCode(cardJson);

            HasQrData = QrBytes != null;
        }

    }
}
