using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Card
{
    /// <summary>
    /// ViewModel para visualização do "Meu Cartão".
    /// </summary>
    public class ViewMyCardViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância do ViewMyCardViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardService">Provedor de BizCard a ser utilizado</param>
        public ViewMyCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;
            NavigateToEditMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<EditMyCardViewModel>());
            LoadGravatarCommand = new MvxCommand(() => GravatarTask = MvxNotifyTask.Create(() => LoadGravatarAsync()));
        }

        /// <summary>
        /// Provedor de BizCard.
        /// </summary>
        private IBizCardService BizCardService { get; }

        /// <summary>
        /// Command para navegar à ViewModel de "Edição" do Cartão.
        /// </summary>
        public IMvxAsyncCommand NavigateToEditMyCardCommand { get; private set; }

        /// <summary>
        /// Command para carregar o Gravatar do Cartão.
        /// </summary>
        public IMvxCommand LoadGravatarCommand { get; private set; }

        private BizCardContent _bizCard;

        /// <summary>
        /// Cartão do Usuário a ser exibido pela View.
        /// </summary>
        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
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

        private MvxNotifyTask _gravatarTask;

        /// <summary>
        /// Task para acompanhamento do processo de busca do Gravatar.
        /// </summary>
        public MvxNotifyTask GravatarTask
        {
            get { return _gravatarTask; }
            set { SetProperty(ref _gravatarTask, value); }
        }

        /// <summary>
        /// Inicializa a ViewModel.
        /// </summary>
        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetMyCardAsync();    // TODO: Separar para uma MvxNotifyTask, para melhor performance

            LoadGravatarCommand.Execute();
        }

        /// <summary>
        /// Carrega o Gravatar do Cartão de Visitas.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        private async Task LoadGravatarAsync(CancellationToken ct = default)
        {
            if (!string.IsNullOrWhiteSpace(BizCard.Email))
                GravatarBytes = await BizCardService.GetGravatarAsync(ct);
        }
    }
}
