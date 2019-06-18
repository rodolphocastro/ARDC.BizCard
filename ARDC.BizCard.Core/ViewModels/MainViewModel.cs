using ARDC.BizCard.Core.ViewModels.Agenda;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    /// <summary>
    /// ViewModel principal, responsável pela navegação do Menu.
    /// </summary>
    public class MainViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância da MaiNViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de Logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de Navegação a ser utilizado</param>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            NavigateToMyCardCommand = new MvxCommand(() => NavigationTask = MvxNotifyTask.Create(() => NavigationService.Navigate<ViewMyCardViewModel>()), () => CanNavigate());
            NavigateToQrCommand = new MvxCommand(() => NavigationTask = MvxNotifyTask.Create(() => NavigationService.Navigate<QrCodeViewModel>()), () => CanNavigate());
            NavigateToAgendaCommand = new MvxCommand(() => NavigationTask = MvxNotifyTask.Create(() => NavigationService.Navigate<AgendaViewModel>()), () => CanNavigate());
        }

        private MvxNotifyTask<bool> _navigationTask;

        /// <summary>
        /// Task para acompanhamento da navegação da MainView.
        /// </summary>
        public MvxNotifyTask<bool> NavigationTask
        {
            get { return _navigationTask; }
            set { SetProperty(ref _navigationTask, value); }
        }


        /// <summary>
        /// Command para navegar à ViewModel de "Meu Cartão".
        /// </summary>
        public IMvxCommand NavigateToMyCardCommand { get; private set; }

        /// <summary>
        /// Command para navegar à ViewModel de "Meu QR Code".
        /// </summary>
        public IMvxCommand NavigateToQrCommand { get; private set; }

        /// <summary>
        /// Command para navegar à ViewModel de "Agenda".
        /// </summary>
        public IMvxCommand NavigateToAgendaCommand { get; private set; }

        /// <summary>
        /// Verifica se a navegação pode ser realizada.
        /// </summary>
        /// <returns>TRUE caso a Task não exista ou esteja finalizada</returns>
        private bool CanNavigate()
        {
            return NavigationTask == null || NavigationTask.IsCompleted;
        }
    }
}
