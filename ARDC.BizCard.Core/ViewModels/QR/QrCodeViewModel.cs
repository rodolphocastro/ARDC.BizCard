using ARDC.BizCard.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.ViewModels.QR
{
    /// <summary>
    /// ViewModel para apresentar o QrCode do Usuário.
    /// </summary>
    public class QrCodeViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Cria uma nova instância da QrCodeViewModel.
        /// </summary>
        /// <param name="logProvider">Provedor de logs a ser utilizado</param>
        /// <param name="navigationService">Provedor de navegação a ser utilizado</param>
        /// <param name="bizCardService">Provedor de BizCards a ser utilizado</param>
        /// <param name="qrCodeService">Provedor de QrCodes a ser utilizado</param>
        public QrCodeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IBizCardService bizCardService, IQrCodeService qrCodeService) : base(logProvider, navigationService)
        {
            BizCardService = bizCardService ?? throw new ArgumentNullException(nameof(bizCardService));
            QrCodeService = qrCodeService ?? throw new ArgumentNullException(nameof(qrCodeService));
            NavigateToHomeCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<LandingViewModel>());
        }

        /// <summary>
        /// Provedor de BizCard.
        /// </summary>
        private IBizCardService BizCardService { get; }

        /// <summary>
        /// Provedor de QrCodes.
        /// </summary>
        private IQrCodeService QrCodeService { get; }

        private byte[] _qrBytes;

        /// <summary>
        /// Bytes que compôem a Imagem do QR Code.
        /// </summary>
        public byte[] QrBytes
        {
            get { return _qrBytes; }
            set { SetProperty(ref _qrBytes, value); }
        }

        private bool _hasQrData = false;

        /// <summary>
        /// Flag indicando que existe um QR Code a ser exibido.
        /// </summary>
        public bool HasQrData
        {
            get { return _hasQrData; }
            set { SetProperty(ref _hasQrData, value); }
        }

        /// <summary>
        /// Command para navegar ao LandingViewModel.
        /// </summary>
        public IMvxAsyncCommand NavigateToHomeCommand { get; private set; }     //  TODO: Verificar necessidade deste Command.

        /// <summary>
        /// Inicializa o ViewModel.
        /// </summary>
        public override async Task Initialize()
        {
            await base.Initialize();

            string cardJson = await BizCardService.GetMyCardAsJSONAsync();

            if (!string.IsNullOrEmpty(cardJson))
                QrBytes = await QrCodeService.CreateQRCode(cardJson);   // TODO: Separar em uma MvxNotifyTask para melhorar performance

            HasQrData = QrBytes != null;
        }

    }
}
