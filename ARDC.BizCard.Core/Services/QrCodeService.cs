using QRCoder;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class QrCodeService : IQrCodeService
    {
        public Task<byte[]> CreateQRCode(string payload, CancellationToken ct)
        {
            var codeGenerator = new QRCodeGenerator();
            QRCodeData qrData = codeGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.M, true);
            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrData);
            byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);

            return Task.FromResult(qrCodeAsBitmapByteArr);
        }
    }
}
