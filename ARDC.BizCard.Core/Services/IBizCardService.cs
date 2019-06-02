using ARDC.BizCard.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public interface IBizCardService
    {
        Task CreateOrEditCardAsync(BizCardContent bizCard, CancellationToken ct = default);

        Task<BizCardContent> GetCardAsync(CancellationToken ct = default);

        Task<string> GetCardAsJSONAsync(CancellationToken ct = default);

        Task<BizCardContent> GetCardFromJSONAsync(string jsonCard, CancellationToken ct = default);
    }
}
