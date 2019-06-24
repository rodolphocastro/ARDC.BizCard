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
        /// <param name="launcherService">Provedor de inicialização de outros Apps</param>
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService, IAppLauncherService launcherService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));
            LauncherService = launcherService ?? throw new ArgumentNullException(nameof(launcherService));

            AddCardToAgendaCommand = new MvxCommand(() => AddCardTask = MvxNotifyTask.Create(() => AddCardAsync()));
            LoadGravatarCommand = new MvxCommand(() => GravatarTask = MvxNotifyTask.Create(() => LoadGravatarAsync()));

            OpenLinkedInCommand = new MvxCommand(() => SocialAppsTask = MvxNotifyTask.Create(() => LauncherService.LaunchLinkedInAsync(BizCard.LinkedIn)), () => CanOpenNewApp());
            OpenPrimaryPhoneCommand = new MvxCommand(() => SocialAppsTask = MvxNotifyTask.Create(() => LauncherService.LaunchPhoneAsync(BizCard.TelefonePrincipal)), () => CanOpenNewApp());
            OpenSecondaryPhoneCommand = new MvxCommand(() => SocialAppsTask = MvxNotifyTask.Create(() => LauncherService.LaunchPhoneAsync(BizCard.TelefoneSecundario)), () => CanOpenNewApp());
            OpenWebsiteCommand = new MvxCommand(() => SocialAppsTask = MvxNotifyTask.Create(() => LauncherService.LaunchWebBrowserAsync(BizCard.Website)), () => CanOpenNewApp());
            OpenEmailCommand = new MvxCommand(() => SocialAppsTask = MvxNotifyTask.Create(() => LauncherService.LaunchEMailAsync(BizCard.Email)), () => CanOpenNewApp());
        }

        /// <summary>
        /// Provedor de CardAgenda.
        /// </summary>
        private IBizCardAgendaService BizCardAgendaService { get; }

        /// <summary>
        /// Provedor de Dialogs.
        /// </summary>
        private IUserDialogs UserDialogsService { get; }

        /// <summary>
        /// Provedor de iniciar outros Apps.
        /// </summary>
        private IAppLauncherService LauncherService { get; }

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

        private MvxNotifyTask _socialAppsTask;

        /// <summary>
        /// Task para acompanhamento do processo de abrir outros Apps.
        /// </summary>
        public MvxNotifyTask SocialAppsTask
        {
            get { return _socialAppsTask; }
            set { SetProperty(ref _socialAppsTask, value); }
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
        /// Command para abrir o LinkedIn do Cartão.
        /// </summary>
        public IMvxCommand OpenLinkedInCommand { get; private set; }

        /// <summary>
        /// Command para abrir o Telefone Primário do Cartão.
        /// </summary>
        public IMvxCommand OpenPrimaryPhoneCommand { get; private set; }

        /// <summary>
        /// Command para abrir o Telefone Secundário do Cartão.
        /// </summary>
        public IMvxCommand OpenSecondaryPhoneCommand { get; private set; }

        /// <summary>
        /// Command para abrir o Website do Cartão.
        /// </summary>
        public IMvxCommand OpenWebsiteCommand { get; private set; }

        /// <summary>
        /// Command para abrir o Email do Cartão.
        /// </summary>
        public IMvxCommand OpenEmailCommand { get; private set; }

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
            if (await BizCardAgendaService.GetCardByNameAsync(BizCard.NomeCompleto) != null)
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

        /// <summary>
        /// Verifica se um novo App pode ser iniciado.
        /// </summary>
        /// <returns>TRUE caso a Task esteja concluída ou não esteja iniciada</returns>
        private bool CanOpenNewApp() => SocialAppsTask == null || SocialAppsTask.IsCompleted;

    }
}
