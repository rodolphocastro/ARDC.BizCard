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
    public class ViewCardViewModel : MvxNavigationViewModel<BizCardContent>
    {
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            AddCardToAgendaCommand = new MvxCommand(() => AddCardTask = MvxNotifyTask.Create(() => AddCardAsync()));
        }

        private IBizCardAgendaService BizCardAgendaService { get; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private MvxNotifyTask _addCardTask;

        public MvxNotifyTask AddCardTask
        {
            get { return _addCardTask; }
            set { SetProperty(ref _addCardTask, value); }
        }

        public IMvxCommand AddCardToAgendaCommand { get; private set; }

        public override void Prepare(BizCardContent parameter)
        {
            BizCard = parameter;
        }

        private async Task AddCardAsync(CancellationToken ct = default)
        {
            // TODO: Pedir confirmação do usuário
            await BizCardAgendaService.AddCardAsync(BizCard, ct);
            await NavigationService.Navigate<AgendaViewModel>();
        }
    }
}
