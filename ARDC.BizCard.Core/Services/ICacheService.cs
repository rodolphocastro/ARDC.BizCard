using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Define os métodos para interação com o Cache da aplicação.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Armazena um objeto no Cache.
        /// </summary>
        /// <param name="key">A chave de armazenamento</param>
        /// <param name="obj">Objeto a ser armazenado</param>
        /// <param name="destination">Cache no qual o objeto deve ser armazenado</param>
        Task StoreObjectAsync(string key, object obj, CacheType destination = CacheType.Memory);

        /// <summary>
        /// Recupera um objeto do Cache.
        /// </summary>
        /// <typeparam name="T">O tipo do objeto a ser recuperado</typeparam>
        /// <param name="key">A chave de armazenamento</param>
        /// <param name="source">Cache no qual o objeto está armazenado</param>
        /// <returns>O objeto encontrado</returns>
        Task<T> RecoverObjectAsync<T>(string key, CacheType source = CacheType.Memory);

        /// <summary>
        /// Invalida um objeto do Cache.
        /// </summary>
        /// <param name="key">A chave de armazenamento</param>
        /// <param name="source">Cache no qual o objeto está armazenado</param>
        Task InvalidateObjectAsync(string key, CacheType source = CacheType.Memory);

        /// <summary>
        /// Recupera no Cache ou busca remotamente uma imagem.
        /// </summary>
        /// <param name="url">A url em que a imagem está disponível</param>
        /// <param name="source">Cache no qual ela deve ser armazenada ou recuperada</param>
        /// <returns>Um array de bytes contendo o bitmap da imagem</returns>
        Task<byte[]> RecoverOrFetchImageAsync(string url, CacheType source);
    }

    /// <summary>
    /// Tipos disponíveis de Cache.
    /// </summary>
    public enum CacheType
    {
        Local,
        User,
        Secure,
        Memory
    }
}
