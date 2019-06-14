using ARDC.BizCard.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Serviço para acesso à Agenda de cartões do usuário.
    /// </summary>
    public class BizCardAgendaService : IBizCardAgendaService
    {
        /// <summary>
        /// Chave para armazenamento da Agenda no cache.
        /// </summary>
        private const string MyAgendaCacheKey = "my_agenda";

        /// <summary>
        /// Agenda de cartões.
        /// </summary>
        private List<BizCardContent> BizCards { get; set; }

        /// <summary>
        /// Provedor de Cache.
        /// </summary>
        private ICacheService CacheService { get; }

        /// <summary>
        /// Cria uma nova instância do serviço.
        /// </summary>
        /// <param name="cacheService">Provedor de cache a ser utilizado</param>
        public BizCardAgendaService(ICacheService cacheService)
        {
            CacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task AddCardAsync(BizCardContent newCard, CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            BizCards.Add(newCard);
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

            if (await GetCardByName(card.NomeCompleto, ct) != null)
                BizCards.Remove(card);

            await UpdateCache();
        }

        public async Task<BizCardContent> GetCardByName(string name, CancellationToken ct)
        {
            if (BizCards == null)
                await InitializeCollection();

            return BizCards.Find(c => c.NomeCompleto.ToUpper() == name.ToUpper());
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

        public async Task<byte[]> GetGravatarAsync(BizCardContent bizCard, CancellationToken ct)
        {
            return await CacheService.RecoverOrFetchImageAsync(bizCard.ToGravatarURI(), CacheType.Local);
        }

        /// <summary>
        /// Inicializa (cria e recupera dados) a Agenda.
        /// </summary>
        private async Task InitializeCollection()   // TODO: Refatorar para que tenha Async na assinatura
        {
            BizCards = new List<BizCardContent>();

            var cacheCards = await CacheService.RecoverObjectAsync<List<BizCardContent>>(MyAgendaCacheKey, CacheType.Local);
            if (cacheCards != null)
                BizCards.AddRange(cacheCards);
        }

        /// <summary>
        /// Atualiza a Agenda no Cache.
        /// </summary>
        private async Task UpdateCache()    // TODO: Refatorar para que tenha Async na assinatura.
        {
            if (BizCards != null)
                await CacheService.StoreObjectAsync(MyAgendaCacheKey, BizCards, CacheType.Local);
        }

    }
}
