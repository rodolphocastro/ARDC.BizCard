using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels
{
    public class EditCardViewModel : MvxNavigationViewModel
    {
        public EditCardViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService;

            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));
            SaveChangesCommand = new MvxCommand(() => SaveChangesTask = MvxNotifyTask.Create(() => SaveChangesAsync()));
        }
        private IBizCardService BizCardService { get; }

        private BizCardContent _bizCard;

        public BizCardContent BizCard
        {
            get { return _bizCard; }
            set { SetProperty(ref _bizCard, value); }
        }

        private MvxNotifyTask _saveChangesTask;

        public MvxNotifyTask SaveChangesTask
        {
            get { return _saveChangesTask; }
            set { SetProperty(ref _saveChangesTask, value); }
        }


        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }

        public IMvxCommand SaveChangesCommand { get; private set; }

        public override async Task Initialize()
        {
            await base.Initialize();

            BizCard = await BizCardService.GetCardAsync();
        }

        private async Task SaveChangesAsync()
        {
            await BizCardService.CreateOrEditCardAsync(BizCard);
        }
    }
}
