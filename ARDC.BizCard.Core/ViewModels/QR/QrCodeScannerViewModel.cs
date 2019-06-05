using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Card;
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
        public QrCodeScannerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService ?? throw new ArgumentNullException(nameof(bizCardService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));

            ReadCardCommand = new MvxCommand<string>((p) => SaveCardTask = MvxNotifyTask.Create(() => LoadCardFromJsonAsync(p)));
        }

        private IBizCardService BizCardService { get; }
        private IUserDialogs UserDialogsService { get; }

        private MvxNotifyTask _saveCardTask;

        public MvxNotifyTask SaveCardTask
        {
            get { return _saveCardTask; }
            set { SetProperty(ref _saveCardTask, value); }
        }

        public IMvxCommand<string> ReadCardCommand { get; private set; }

        private async Task LoadCardFromJsonAsync(string payload, CancellationToken ct = default)
        {
            var bizCard = await BizCardService.GetCardFromJSONAsync(payload, ct);

            if (bizCard.HasData())
            {
                UserDialogsService.Toast("Cartão lido", TimeSpan.FromSeconds(2));

                await NavigationService.Navigate<ViewCardViewModel, BizCardContent>(bizCard);
            }
            else
            {
                UserDialogsService.Toast("Não foi processar o QR Code");
            }            
        }
    }
}
