using ARDC.BizCard.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class BizCardAgendaService : IBizCardAgendaService
    {
        private const string MyAgendaCacheKey = "my_agenda";

        private List<BizCardContent> BizCards { get; set; }

        private ICacheService CacheService { get; }

        public BizCardAgendaService(ICacheService cacheService)
        {
            CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task InitializeAsync()
        {
            await InitializeCollection();
        }

        public async Task AddCardAsync(BizCardContent newCard, CancellationToken ct)
        {
            BizCards.Add(newCard);
            await UpdateCache();
        }

        public Task<IList<BizCardContent>> GetCardsAsync(CancellationToken ct)
        {
            return Task.FromResult<IList<BizCardContent>>(BizCards);
        }

        public async Task RemoveCardAsync(BizCardContent card, CancellationToken ct)
        {
            if (await GetCardByName(card.NomeCompleto, ct) != null)
                BizCards.Remove(card);

            await UpdateCache();
        }

        public Task<BizCardContent> GetCardByName(string name, CancellationToken ct)
        {
            return Task.FromResult(BizCards.Find(c => c.NomeCompleto.ToUpper() == name.ToUpper()));
        }

        public async Task<byte[]> GetGravatarAsync(BizCardContent bizCard, CancellationToken ct)
        {
            return await CacheService.RecoverOrFetchImageAsync(bizCard.ToGravatarURI(), CacheType.Local);
        }

        private async Task InitializeCollection()
        {
            BizCards = new List<BizCardContent>();

            var cacheCards = await CacheService.RecoverObjectAsync<List<BizCardContent>>(MyAgendaCacheKey, CacheType.Local);
            if (cacheCards != null)
                BizCards.AddRange(cacheCards);
        }

        private async Task UpdateCache()
        {
            if (BizCards != null)
                await CacheService.StoreObjectAsync(MyAgendaCacheKey, BizCards, CacheType.Local);
        }
    }
}
