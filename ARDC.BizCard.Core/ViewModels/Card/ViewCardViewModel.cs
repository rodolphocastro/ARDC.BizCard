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
    public class ViewCardViewModel : MvxNavigationViewModel<BizCardContent>
    {
        public ViewCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));

            AddCardToAgendaCommand = new MvxCommand(() => AddCardTask = MvxNotifyTask.Create(() => AddCardAsync()));
            LoadGravatarCommand = new MvxCommand(() => GravatarTask = MvxNotifyTask.Create(() => LoadGravatarAsync()));
        }

        private IBizCardAgendaService BizCardAgendaService { get; }

        private IUserDialogs UserDialogsService { get; }

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

        private MvxNotifyTask _gravatarTask;

        public MvxNotifyTask GravatarTask
        {
            get { return _gravatarTask; }
            set { SetProperty(ref _gravatarTask, value); }
        }

        private byte[] _gravatarBytes;

        public byte[] GravatarBytes
        {
            get { return _gravatarBytes; }
            set { SetProperty(ref _gravatarBytes, value); }
        }

        private bool _isOnAgenda;

        public bool IsOnAgenda
        {
            get { return _isOnAgenda; }
            set { SetProperty(ref _isOnAgenda, value); }
        }        

        public IMvxCommand AddCardToAgendaCommand { get; private set; }

        public IMvxCommand LoadGravatarCommand { get; private set; }

        public override void Prepare(BizCardContent parameter)
        {
            BizCard = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            IsOnAgenda = await BizCardAgendaService.IsCardOnAgendaAsync(BizCard);

            LoadGravatarCommand.Execute();
        }

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

        private async Task LoadGravatarAsync(CancellationToken ct = default)
        {
            if (!string.IsNullOrWhiteSpace(BizCard.Email))
                GravatarBytes = await BizCardAgendaService.GetGravatarAsync(BizCard);
        }
    }
}
