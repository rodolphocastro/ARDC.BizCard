using ARDC.BizCard.Core.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class BizCardService : IBizCardService
    {
        private const string MyBizCardCacheKey = "my_bizcard";

        private BizCardContent BizCard { get; set; }

        private ICacheService CacheService { get; }

        public BizCardService(ICacheService cacheService)
        {
            CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task CreateOrEditCardAsync(BizCardContent bizCard, CancellationToken ct)
        {
            BizCard = bizCard;

            await CacheService.StoreObjectAsync(MyBizCardCacheKey, BizCard, CacheType.Local);
        }

        public async Task<BizCardContent> GetCardAsync(CancellationToken ct)
        {
            if (BizCard == null)
                BizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);

            return BizCard ?? new BizCardContent();
        }

        public async Task<string> GetCardAsJSONAsync(CancellationToken ct = default)
        {
            if (BizCard == null)
                BizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);

            return JsonConvert.SerializeObject(BizCard);
        }
    }
}
