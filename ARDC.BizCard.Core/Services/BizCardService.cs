using ARDC.BizCard.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class BizCardService : IBizCardService
    {
        private BizCardContent BizCard { get; set; }

        public Task CreateOrEditCardAsync(BizCardContent bizCard, CancellationToken ct)
        {
            BizCard = bizCard;

            return Task.CompletedTask;
        }

        public Task<BizCardContent> GetCardAsync(CancellationToken ct)
        {
            return Task.FromResult(BizCard ?? new BizCardContent());
        }

        public Task GetQRCodeAsync(BizCardContent bizCard, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
