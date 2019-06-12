using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Agenda
{
    /// <summary>
    /// ViewModel para interagir com a Agenda de Cartões.
    /// </summary>
    public class AgendaViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância da AgendaViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardAgendaService">Provedor de CardAgenda a ser utilizado</param>
        /// <param name="userDialogsService">Provedor de Dialogs a ser utilizado</param>
        public AgendaViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));

            BizCards = new MvxObservableCollection<BizCardContent>();
            AddCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
            DeleteCardCommand = new MvxCommand<BizCardContent>((b) => DeleteCardTask = MvxNotifyTask.Create(() => DeleteCardAsync(b)));
            DetailCardCommand = new MvxCommand<BizCardContent>((b) => DetailCardTask = MvxNotifyTask.Create(() => NavigateToCardDetails(b)));
        }

        /// <summary>
        /// Provedor de CardAgenda.
        /// </summary>
        private IBizCardAgendaService BizCardAgendaService { get; }

        /// <summary>
        /// Provedor de Dialogs.
        /// </summary>
        private IUserDialogs UserDialogsService { get; }

        private MvxObservableCollection<BizCardContent> _bizCards;

        /// <summary>
        /// Coleção de BizCardContent visível á View.
        /// </summary>
        public MvxObservableCollection<BizCardContent> BizCards
        {
            get { return _bizCards; }
            set { SetProperty(ref _bizCards, value); }
        }

        private MvxNotifyTask _detailCardTask;

        /// <summary>
        /// Task para acompanhamento do processo de "Detalhamento" de Cartões.
        /// </summary>
        public MvxNotifyTask DetailCardTask
        {
            get { return _detailCardTask; }
            set { SetProperty(ref _detailCardTask, value); }
        }

        private MvxNotifyTask _deleteCardTask;

        /// <summary>
        /// Task para acompanhamento do processo de "Remoção" de Cartões.
        /// </summary>
        public MvxNotifyTask DeleteCardTask
        {
            get { return _deleteCardTask; }
            set { SetProperty(ref _deleteCardTask, value); }
        }

        /// <summary>
        /// Command para "Adicionar" um Cartão.
        /// </summary>
        public IMvxAsyncCommand AddCardCommand { get; private set; }

        /// <summary>
        /// Command para detalhar um Cartão.
        /// </summary>
        public IMvxCommand<BizCardContent> DetailCardCommand { get; private set; }

        /// <summary>
        /// Command para deletar um Cartão.
        /// </summary>
        public IMvxCommand<BizCardContent> DeleteCardCommand { get; private set; }

        /// <summary>
        /// Inicializa a ViewModel.
        /// </summary>
        public override async Task Initialize()
        {
            BizCards.Clear();

            await base.Initialize();

            BizCards.AddRange(await BizCardAgendaService.GetCardsAsync());  // TODO: Separar para uma MvxNotifyTask, para melhor performance.
        }

        /// <summary>
        /// Navega á ViewModel de "Detalhar Cartâo".
        /// </summary>
        /// <param name="bizcard">BizCardContent a ser detalhado</param>
        private async Task NavigateToCardDetails(BizCardContent bizcard)
        {
            await NavigationService.Navigate<ViewCardViewModel, BizCardContent>(bizcard);
        }

        /// <summary>
        /// Deleta um BizCardContent da Agenda.
        /// </summary>
        /// <param name="bizCard">BizCardContent a ser deletado</param>
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
