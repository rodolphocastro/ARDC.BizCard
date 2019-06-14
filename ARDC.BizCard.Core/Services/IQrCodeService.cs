using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Define os métodos para criação de QR Codes.
    /// </summary>
    public interface IQrCodeService
    {
        /// <summary>
        /// Gera um QR Code para um payload específico.
        /// </summary>
        /// <param name="payload">O payload a ser carregado no QR Code</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>Um array de bytes contendo o QR Code gerado</returns>
        Task<byte[]> CreateQRCodeAsync(string payload, CancellationToken ct = default);
    }
}