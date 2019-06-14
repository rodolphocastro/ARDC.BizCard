using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Card
{
    /// <summary>
    /// ViewModel para exição do Cartão do Usuário.
    /// </summary>
    public class EditMyCardViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância de EditMyCardViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardService">Provedor de BizCard a ser utilizado</param>
        /// <param name="userDialogsService">Provedor de Dialogs a ser utilizado</param>
        public EditMyCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMyBizCardService bizCardService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;
            UserDialogsService = userDialogsService;

            NavigateToViewMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
            SaveChangesCommand = new MvxCommand(() => SaveChangesTask = MvxNotifyTask.Create(() => SaveChangesAsync()));
        }

        /// <summary>
        /// Provedor de BizCard.
        /// </summary>
        private IMyBizCardService BizCardService { get; }

        /// <summary>
        /// Provedor de Dialogs.
        /// </summary>
        private IUserDialogs UserDialogsService { get; }

        private BizCardContent _bizCard;

        /// <summary>
        /// BizCard do Usuário a ser editado pela View.
        /// </summary>
        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private MvxNotifyTask _saveChangesTask;

        /// <summary>
        /// Task para acompanhamento do processo de "Salvar Alterações".
        /// </summary>
        public MvxNotifyTask SaveChangesTask
        {
            get { return _saveChangesTask; }
            set { SetProperty(ref _saveChangesTask, value); }
        }

        /// <summary>
        /// Command para navegar à ViewModel de "Visualizar meu Cartão".
        /// </summary>
        public IMvxAsyncCommand NavigateToViewMyCardCommand { get; private set; }

        /// <summary>
        /// Command para salvar alterações ao Cartão.
        /// </summary>
        public IMvxCommand SaveChangesCommand { get; private set; }

        /// <summary>
        /// Inicializa a ViewModel.
        /// </summary>
        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetMyCardAsync();    // TODO: Separar a uma MvxNotifyTask para melhor performance.
        }

        /// <summary>
        /// Salva as alterações realizadas ao Cartão.
        /// </summary>
        private async Task SaveChangesAsync()
        {
            await BizCardService.CreateOrEditMyCardAsync(BizCard);

            UserDialogsService.Toast("Alterações Salvas");

            await NavigateToViewMyCardCommand.ExecuteAsync();
        }
    }
}
