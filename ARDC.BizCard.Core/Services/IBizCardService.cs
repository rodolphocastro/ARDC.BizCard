using ARDC.BizCard.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Define os métodos para acesso ao cartão do usuário.
    /// </summary>
    public interface IBizCardService
    {
        /// <summary>
        /// Criar ou Editar o cartão.
        /// </summary>
        /// <param name="bizCard">Os dados a serem criados/atualizados</param>
        /// <param name="ct">Token para controle de cancelamento</param>
        Task CreateOrEditMyCardAsync(BizCardContent bizCard, CancellationToken ct = default);

        /// <summary>
        /// Recuperar o cartão.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>O Cartão do usuário</returns>
        /// <remarks>Caso o cartão ainda nào exista um cartão vazio deve ser retornado</remarks>
        Task<BizCardContent> GetMyCardAsync(CancellationToken ct = default);

        /// <summary>
        /// Recupera uma representação JSON do cartão.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>Uma string contendo o cartão serializado em padrão JSON</returns>
        Task<string> GetMyCardAsJSONAsync(CancellationToken ct = default);

        /// <summary>
        /// Recupera o Gravatar (Imagem) do usuário.
        /// </summary>
        /// <param name="ct">Token para controle de cancelamento</param>
        /// <returns>Array de bytes contendo a imagem do Gravatar</returns>
        Task<byte[]> GetGravatarAsync(CancellationToken ct = default);
    }
}
