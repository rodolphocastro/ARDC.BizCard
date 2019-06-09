using System.Threading.Tasks;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Agenda;
using ARDC.BizCard.Core.ViewModels.Card;
using ARDC.BizCard.Core.ViewModels.QR;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace ARDC.BizCard.Core.ViewModels
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ICacheService cacheService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            CacheService = cacheService ?? throw new System.ArgumentNullException(nameof(cacheService));
            BizCardService = bizCardService ?? throw new System.ArgumentNullException(nameof(bizCardService));
            InitializeServicesCommand = new MvxCommand(() => InitServicesTask = MvxNotifyTask.Create(() => InitServices()));

            NavigateToMyCardCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<ViewMyCardViewModel>());
            NavigateToQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeViewModel>());
            NavigateToReadQrCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<QrCodeScannerViewModel>());
            NavigateToAgendaCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<AgendaViewModel>());
        }

        public IMvxAsyncCommand NavigateToMyCardCommand { get; private set; }

        public IMvxAsyncCommand NavigateToQrCommand { get; private set; }

        public IMvxAsyncCommand NavigateToReadQrCommand { get; private set; }

        public IMvxAsyncCommand NavigateToAgendaCommand { get; private set; }

        public IMvxCommand InitializeServicesCommand { get; private set; }

        private MvxNotifyTask _initServicesTask;

        public MvxNotifyTask InitServicesTask
        {
            get { return _initServicesTask; }
            set { SetProperty(ref _initServicesTask, value); }
        }

        private ICacheService CacheService { get; }
        private IBizCardService BizCardService { get; }

        public override Task Initialize()
        {
            InitializeServicesCommand.Execute();

            return base.Initialize();
        }

        private async Task InitServices()
        {
            await CacheService.InitializeAsync();
            await BizCardService.InitializeAsync();
        }
    }
}
