using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Card;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.Agenda
{
    public class AgendaViewModel : MvxNavigationViewModel
    {
        public AgendaViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new System.ArgumentNullException(nameof(bizCardAgendaService));

            BizCards = new MvxObservableCollection<BizCardContent>();
            DetailCardCommand = new MvxCommand<BizCardContent>((b) => DetailCardTask = MvxNotifyTask.Create(() => NavigateToCardDetails(b)));
        }

        private IBizCardAgendaService BizCardAgendaService { get; }

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

        public IMvxCommand<BizCardContent> DetailCardCommand { get; private set; }

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
    }
}
