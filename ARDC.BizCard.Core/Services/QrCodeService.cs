using QRCoder;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Serviço para a geração de QR Codes do App.
    /// </summary>
    public class QrCodeService : IQrCodeService
    {
        public Task<byte[]> CreateQRCodeAsync(string payload, CancellationToken ct)
        {
            var codeGenerator = new QRCodeGenerator();
            QRCodeData qrData = codeGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.L, true);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(10);

            return Task.FromResult(qrCodeAsBitmapByteArr);
        }
    }
}
