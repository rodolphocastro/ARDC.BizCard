using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Define os métodos para iniciar outros Aplicativos.
    /// </summary>
    public interface IAppLauncherService
    {
        /// <summary>
        /// Abre um perfil no LinkedIn.
        /// </summary>
        /// <param name="profile">O perfil a ser aberto</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LaunchLinkedInAsync(string profile, CancellationToken ct = default);

        /// <summary>
        /// Abre um número no discador.
        /// </summary>
        /// <param name="phoneNumber">O número a ser discado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LaunchPhoneAsync(string phoneNumber, CancellationToken ct = default);

        /// <summary>
        /// Abre um link no Browser.
        /// </summary>
        /// <param name="url">URL a ser aberta</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LaunchWebBrowserAsync(string url, CancellationToken ct = default);

        /// <summary>
        /// Cria um novo e-mail.
        /// </summary>
        /// <param name="mailAddress">Endereço ao qual será enviado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task LaunchEMailAsync(string mailAddress, CancellationToken ct = default);
    }
}
