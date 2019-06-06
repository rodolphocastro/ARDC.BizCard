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

        public async Task AddCardAsync(BizCardContent newCard, CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            BizCards.Add(newCard);  // TODO: Validar duplicatas
            await UpdateCache();
        }

        public async Task<IList<BizCardContent>> GetCardsAsync(CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            return BizCards;
        }

        public async Task RemoveCardAsync(BizCardContent card, CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            BizCards.Remove(card); // TODO: Validar casos inexistentes
            await UpdateCache();
        }

        public async Task<BizCardContent> GetCardByName(string name, CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            return BizCards.Find(c => c.NomeCompleto.ToUpper() == name.ToUpper());
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
