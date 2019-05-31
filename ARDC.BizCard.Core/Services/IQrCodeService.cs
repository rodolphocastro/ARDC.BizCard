using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public interface IQrCodeService
    {
        Task<byte[]> CreateQRCode(string payload, CancellationToken ct = default);
    }
}