using ARDC.BizCard.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public interface IBizCardService
    {
        Task CreateOrEditMyCardAsync(BizCardContent bizCard, CancellationToken ct = default);

        Task<BizCardContent> GetMyCardAsync(CancellationToken ct = default);

        Task<string> GetMyCardAsJSONAsync(CancellationToken ct = default);

        Task<BizCardContent> GetCardFromJSONAsync(string jsonCard, CancellationToken ct = default);

        Task<byte[]> GetGravatarAsync(CancellationToken ct = default);
    }
}
