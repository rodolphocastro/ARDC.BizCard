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

        private BizCardContent MyBizCard { get; set; }

        private ICacheService CacheService { get; }

        public BizCardService(ICacheService cacheService)
        {
            CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task InitializeAsync()
        {
            MyBizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);
        }

        public async Task CreateOrEditMyCardAsync(BizCardContent bizCard, CancellationToken ct)
        {
            MyBizCard = bizCard;

            await CacheService.StoreObjectAsync(MyBizCardCacheKey, MyBizCard, CacheType.Local);
        }

        public Task<BizCardContent> GetMyCardAsync(CancellationToken ct)
        {
            return Task.FromResult(MyBizCard ?? new BizCardContent());
        }

        public Task<string> GetMyCardAsJSONAsync(CancellationToken ct)
        {
            return Task.FromResult(JsonConvert.SerializeObject(MyBizCard));
        }

        public Task<BizCardContent> GetCardFromJSONAsync(string jsonCard, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(jsonCard))
                return Task.FromResult(new BizCardContent());

            try
            {
                var newBizCard = JsonConvert.DeserializeObject<BizCardContent>(jsonCard);
                return Task.FromResult(newBizCard);
            }
            catch (Exception)
            {
                return Task.FromResult(new BizCardContent());
                throw;
            }
        }

        public async Task<byte[]> GetGravatarAsync(CancellationToken ct)
        {
            return await CacheService.RecoverOrFetchImageAsync(MyBizCard.ToGravatarURI(), CacheType.Local);
        }        
    }
}
