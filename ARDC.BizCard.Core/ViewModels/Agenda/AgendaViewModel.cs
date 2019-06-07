using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Agenda
{
    public class AgendaViewModel : MvxNavigationViewModel
    {
        public AgendaViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new System.ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new System.ArgumentNullException(nameof(userDialogsService));

            BizCards = new MvxObservableCollection<BizCardContent>();
            AddCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
            DeleteCardCommand = new MvxCommand<BizCardContent>((b) => DeleteCardTask = MvxNotifyTask.Create(() => DeleteCardAsync(b)));
            DetailCardCommand = new MvxCommand<BizCardContent>((b) => DetailCardTask = MvxNotifyTask.Create(() => NavigateToCardDetails(b)));
        }

        private IBizCardAgendaService BizCardAgendaService { get; }
        private IUserDialogs UserDialogsService { get; }

        private MvxObservableCollection<BizCardContent> _bizCards;

        public MvxObservableCollection<BizCardContent> BizCards
        {
            get { return _bizCards; }
            set { SetProperty(ref _bizCards, value); }
        }

        private MvxNotifyTask _detailCardTask;

        public MvxNotifyTask DetailCardTask
        {
            get { return _detailCardTask; }
            set { SetProperty(ref _detailCardTask, value); }
        }

        private MvxNotifyTask _deleteCardTask;

        public MvxNotifyTask DeleteCardTask
        {
            get { return _deleteCardTask; }
            set { SetProperty(ref _deleteCardTask, value); }
        }

        public IMvxAsyncCommand AddCardCommand { get; private set; }

        public IMvxCommand<BizCardContent> DetailCardCommand { get; private set; }

        public IMvxCommand<BizCardContent> DeleteCardCommand { get; private set; }

        public override async Task Initialize()
        {
            BizCards.Clear();

            await base.Initialize();

            BizCards.AddRange(await BizCardAgendaService.GetCardsAsync());
        }

        private async Task NavigateToCardDetails(BizCardContent bizcard)
        {
            await NavigationService.Navigate<ViewCardViewModel, BizCardContent>(bizcard);
        }

        private async Task DeleteCardAsync(BizCardContent bizCard)
        {
            if (await UserDialogsService.ConfirmAsync("Deseja remover este cartão?", bizCard.NomeCompleto))
            {
                await BizCardAgendaService.RemoveCardAsync(bizCard);
                BizCards.ReplaceWith(await BizCardAgendaService.GetCardsAsync());
            }
        }
    }
}
