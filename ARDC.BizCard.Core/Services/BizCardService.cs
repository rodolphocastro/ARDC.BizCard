using Akavache;
using ARDC.BizCard.Core.Models;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class BizCardService : IBizCardService
    {
        private BizCardContent BizCard { get; set; }

        public async Task CreateOrEditCardAsync(BizCardContent bizCard, CancellationToken ct)
        {
            BizCard = bizCard;

            await BlobCache.UserAccount.InsertObject("my_biz_card", BizCard);
        }

        public async Task<BizCardContent> GetCardAsync(CancellationToken ct)
        {
            try
            {
                BizCard = await BlobCache.UserAccount.GetObject<BizCardContent>("my_biz_card");
            }
            catch (KeyNotFoundException)
            {
                BizCard = new BizCardContent();
            }

            return BizCard;
        }

        public Task GetQRCodeAsync(BizCardContent bizCard, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
