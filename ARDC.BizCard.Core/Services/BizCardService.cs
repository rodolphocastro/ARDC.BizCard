using ARDC.BizCard.Core.Models;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Serviço para acesso ao Cartão do usuário.
    /// </summary>
    public class BizCardService : IBizCardService
    {
        /// <summary>
        /// Chave para armazenamento do cartão no cache.
        /// </summary>
        private const string MyBizCardCacheKey = "my_bizcard";

        /// <summary>
        /// Cartão do usuário.
        /// </summary>
        private BizCardContent MyBizCard { get; set; }

        /// <summary>
        /// Provedor de Cache.
        /// </summary>
        private ICacheService CacheService { get; }

        /// <summary>
        /// Cria uma nova instância do serviço.
        /// </summary>
        /// <param name="cacheService">Provedor de Cache a ser utilizado</param>
        public BizCardService(ICacheService cacheService)
        {
            CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task CreateOrEditMyCardAsync(BizCardContent bizCard, CancellationToken ct)
        {
            MyBizCard = bizCard;

            await CacheService.StoreObjectAsync(MyBizCardCacheKey, MyBizCard, CacheType.Local);
        }

        public async Task<BizCardContent> GetMyCardAsync(CancellationToken ct)
        {
            if (MyBizCard == null)
                MyBizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);

            return MyBizCard ?? new BizCardContent();
        }

        public async Task<string> GetMyCardAsJSONAsync(CancellationToken ct)
        {
            if (MyBizCard == null)
                MyBizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);

            return JsonConvert.SerializeObject(MyBizCard);
        }

        public async Task<byte[]> GetGravatarAsync(CancellationToken ct)
        {
            if (MyBizCard == null)
                MyBizCard = await CacheService.RecoverObjectAsync<BizCardContent>(MyBizCardCacheKey, CacheType.Local);

            return await CacheService.RecoverOrFetchImageAsync(MyBizCard.ToGravatarURI(), CacheType.Local);
        }
    }
}
