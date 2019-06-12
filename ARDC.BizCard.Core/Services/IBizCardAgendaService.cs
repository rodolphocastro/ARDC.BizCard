using ARDC.BizCard.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Define os métodos para interação com a agenda de cartões do usuário.
    /// </summary>
    public interface IBizCardAgendaService
    {
        /// <summary>
        /// Adiciona um novo cartão à agenda.
        /// </summary>
        /// <param name="newCard">O cartão a ser adicionado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task AddCardAsync(BizCardContent newCard, CancellationToken ct = default);

        /// <summary>
        /// Remove um cartão da agenda.
        /// </summary>
        /// <param name="card">O cartão a ser removido</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task RemoveCardAsync(BizCardContent card, CancellationToken ct = default);

        /// <summary>
        /// Obtem a agenda de cartões.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>Uma lista contendo os cartões presentes na agenda</returns>
        Task<IList<BizCardContent>> GetCardsAsync(CancellationToken ct = default);

        /// <summary>
        /// Busca um cartão com base no nome completo da pessoa.
        /// </summary>
        /// <param name="name">O nome a ser buscado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O cartão encontrado</returns>
        Task<BizCardContent> GetCardByName(string name, CancellationToken ct = default);    //TODO:

        /// <summary>
        /// Obtem o Gravatar de um cartão.
        /// </summary>
        /// <param name="bizCard">O cartão a ter o Gravatar buscado</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>Um array de bytes contendo a imagem do Gravatar do cartão</returns>
        Task<byte[]> GetGravatarAsync(BizCardContent bizCard, CancellationToken ct = default);
    }
}
