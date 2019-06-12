using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Agenda;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Card
{
    /// <summary>
    /// ViewModel para a visualização de um Cartão de terceiros.
    /// </summary>
    public class ViewCardViewModel : MvxNavigationViewModel<BizCardContent>
    {
        /// <summary>
        /// Cria uma nova instância da ViewCardViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardAgendaService">Provedor de CardAgenda a ser utilizado</param>
        /// <param name="userDialogsService">Provedor de Dialogs a ser utilizado</param>
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));

            AddCardToAgendaCommand = new MvxCommand(() => AddCardTask = MvxNotifyTask.Create(() => AddCardAsync()));
            LoadGravatarCommand = new MvxCommand(() => GravatarTask = MvxNotifyTask.Create(() => LoadGravatarAsync()));
        }

        /// <summary>
        /// Provedor de CardAgenda.
        /// </summary>
        private IBizCardAgendaService BizCardAgendaService { get; }

        /// <summary>
        /// Provedor de Dialogs.
        /// </summary>
        private IUserDialogs UserDialogsService { get; }

        private BizCardContent _bizCard;

        /// <summary>
        /// Cartão a ser exibido pela View.
        /// </summary>
        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private MvxNotifyTask _addCardTask;

        /// <summary>
        /// Task para acompanhamento do processo de "Adicionar Cartão".
        /// </summary>
        public MvxNotifyTask AddCardTask
        {
            get { return _addCardTask; }
            set { SetProperty(ref _addCardTask, value); }
        }

        private MvxNotifyTask _gravatarTask;

        /// <summary>
        /// Task para acompanhamento do processo de buscar o Gravatar.
        /// </summary>
        public MvxNotifyTask GravatarTask
        {
            get { return _gravatarTask; }
            set { SetProperty(ref _gravatarTask, value); }
        }

        private byte[] _gravatarBytes;

        /// <summary>
        /// Bytes para apresentação da imagem do Gravatar pela View.
        /// </summary>
        public byte[] GravatarBytes
        {
            get { return _gravatarBytes; }
            set { SetProperty(ref _gravatarBytes, value); }
        }

        /// <summary>
        /// Command para adicionar o cartão exibido à Agenda.
        /// </summary>
        public IMvxCommand AddCardToAgendaCommand { get; private set; }

        /// <summary>
        /// Command para carregar o Gravatar do Cartão.
        /// </summary>
        public IMvxCommand LoadGravatarCommand { get; private set; }

        /// <summary>
        /// Prepara a ViewModel para exibição.
        /// </summary>
        /// <param name="parameter">O cartão a ser exibido</param>
        public override void Prepare(BizCardContent parameter)
        {
            BizCard = parameter;
        }

        /// <summary>
        /// Inicializa a ViewModel.
        /// </summary>
        public override async Task Initialize()
        {
            await base.Initialize();

            LoadGravatarCommand.Execute();
        }

        /// <summary>
        /// Adiciona um Cartão à Agenda.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        private async Task AddCardAsync(CancellationToken ct = default)
        {
            if (await BizCardAgendaService.GetCardByName(BizCard.NomeCompleto) != null)
            {
                if (await UserDialogsService.ConfirmAsync("Já existe um cartão com este nome, deseja adiciona-lo novamente?", "Cartão Repetido", "Adicionar"))
                {
                    await BizCardAgendaService.AddCardAsync(BizCard, ct);
                    await NavigationService.Navigate<AgendaViewModel>();
                }
            }
            else
            {
                await BizCardAgendaService.AddCardAsync(BizCard, ct);
                await NavigationService.Navigate<AgendaViewModel>();
            }
        }

        /// <summary>
        /// Buscar o Gravatar do Cartão exibido.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        private async Task LoadGravatarAsync(CancellationToken ct = default)
        {
            if (!string.IsNullOrWhiteSpace(BizCard.Email))
                GravatarBytes = await BizCardAgendaService.GetGravatarAsync(BizCard);
        }
    }
}
