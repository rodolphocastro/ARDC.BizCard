using Acr.UserDialogs;
using ARDC.BizCard.Core.Models;
using ARDC.BizCard.Core.Services;
using ARDC.BizCard.Core.ViewModels.Card;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.QR
{
    /// <summary>
    /// ViewModel para Escanear QR Codes.
    /// </summary>
    public class QrCodeScannerViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância do QrCodeScannerViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardService">Provedor de acesso a card a ser utilizado</param>
        public QrCodeScannerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardAgendaService bizCardAgendaService, IUserDialogs userDialogsService) : base(logProvider, navigationService)
        {
            BizCardAgendaService = bizCardAgendaService ?? throw new ArgumentNullException(nameof(bizCardAgendaService));
            UserDialogsService = userDialogsService ?? throw new ArgumentNullException(nameof(userDialogsService));

            ShowCardMessageCommand = new MvxCommand<BizCardContent>((c) => MessageTask = MvxNotifyTask.Create(() => ShowMessageAsync(c)), (c) => CanShowMessage(c));
            ReadCardCommand = new MvxCommand<string>((p) => SaveCardTask = MvxNotifyTask.Create(() => LoadCardFromJsonAsync(p)));
        }

        /// <summary>
        /// Provedor de Agenda.
        /// </summary>
        private IBizCardAgendaService BizCardAgendaService { get; }

        /// <summary>
        /// Provedor de Dialogs com o usuário.
        /// </summary>
        private IUserDialogs UserDialogsService { get; }

        private MvxNotifyTask _saveCardTask;

        /// <summary>
        /// Task para acompanhamento do processo de salvar cards.
        /// </summary>
        public MvxNotifyTask SaveCardTask
        {
            get { return _saveCardTask; }
            set { SetProperty(ref _saveCardTask, value); }
        }

        private MvxNotifyTask _messageTask;

        /// <summary>
        /// Task para acompanhamento da exibição da mensagem de compartilhamento.
        /// </summary>
        public MvxNotifyTask MessageTask
        {
            get { return _messageTask; }
            set { SetProperty(ref _messageTask, value); }
        }

        /// <summary>
        /// Command para realizar a leitura do Card.
        /// </summary>
        public IMvxCommand<string> ReadCardCommand { get; private set; }

        /// <summary>
        /// Command para exibir a mensagem compartilhada pelo Cartão.
        /// </summary>
        public IMvxCommand<BizCardContent> ShowCardMessageCommand { get; private set; }

        /// <summary>
        /// Recupera um BizCardContent a partir de um JSON.
        /// </summary>
        /// <param name="payload">JSON a ser lido</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        private async Task LoadCardFromJsonAsync(string payload, CancellationToken ct = default)
        {
            var bizCard = await BizCardAgendaService.GetCardFromJSONAsync(payload, ct);

            if (bizCard.HasData())
            {
                ShowCardMessageCommand.Execute(bizCard);
                await NavigationService.Navigate<ViewCardViewModel, BizCardContent>(bizCard);
            }
        }

        /// <summary>
        /// Verifica se a Mensagem pode ser exibida ou não.
        /// </summary>
        /// <returns>TRUE caso exista uma mensagem a ser exibida</returns>
        private bool CanShowMessage(BizCardContent card) => !string.IsNullOrWhiteSpace(card.Mensagem);

        /// <summary>
        /// Exibe a mensagem do Cartão.
        /// </summary>
        private async Task ShowMessageAsync(BizCardContent card) => await UserDialogsService.AlertAsync(card.Mensagem, card.NomeCompleto);
    }
}
