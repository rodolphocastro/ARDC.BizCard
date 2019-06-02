using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.QR
{
    public class QrCodeScannerViewModel : MvxNavigationViewModel
    {
        public QrCodeScannerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService ?? throw new ArgumentNullException(nameof(bizCardService));

            ReadCardCommand = new MvxCommand<string>((p) => SaveCardTask = MvxNotifyTask.Create(() => LoadCardFromJsonAsync(p)));
        }

        private IBizCardService BizCardService { get; }

        private MvxNotifyTask _saveCardTask;

        public MvxNotifyTask SaveCardTask
        {
            get { return _saveCardTask; }
            set { SetProperty(ref _saveCardTask, value); }
        }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        public IMvxCommand<string> ReadCardCommand { get; private set; }

        private async Task LoadCardFromJsonAsync(string payload, CancellationToken ct = default)
        {
            BizCard = await BizCardService.GetCardFromJSONAsync(payload);
        }
    }
}
