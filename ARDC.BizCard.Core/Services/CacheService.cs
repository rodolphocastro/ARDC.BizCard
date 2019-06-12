using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    /// <summary>
    /// Serviço para acesso ao Cache do App.
    /// </summary>
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Cria uma nova instância do serviço de Cache.
        /// </summary>
        public CacheService()
        {
            Registrations.Start("ArdcBizCard");    // Inicializa o Akavache
        }

        public async Task InvalidateObjectAsync(string key, CacheType source)
        {
            switch (source)
            {
                case CacheType.Local:
                    await BlobCache.LocalMachine.Invalidate(key);
                    break;
                case CacheType.User:
                    await BlobCache.UserAccount.Invalidate(key);
                    break;
                case CacheType.Secure:
                    await BlobCache.Secure.Invalidate(key);
                    break;
                case CacheType.Memory:
                    await BlobCache.InMemory.Invalidate(key);
                    break;
            }
        }

        public async Task<T> RecoverObjectAsync<T>(string key, CacheType source)
        {
            try
            {
                T cacheObject;

                switch (source)
                {
                    case CacheType.Local:
                        cacheObject = await BlobCache.LocalMachine.GetObject<T>(key);
                        break;
                    case CacheType.User:
                        cacheObject = await BlobCache.UserAccount.GetObject<T>(key);
                        break;
                    case CacheType.Secure:
                        cacheObject = await BlobCache.Secure.GetObject<T>(key);
                        break;
                    case CacheType.Memory:
                        cacheObject = await BlobCache.InMemory.GetObject<T>(key);
                        break;
                    default:
                        cacheObject = default;
                        break;
                }

                return cacheObject;
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        public async Task StoreObjectAsync(string key, object obj, CacheType destination)
        {
            switch (destination)
            {
                case CacheType.Local:
                    await BlobCache.LocalMachine.InsertObject(key, obj);
                    break;
                case CacheType.User:
                    await BlobCache.UserAccount.InsertObject(key, obj);
                    break;
                case CacheType.Secure:
                    await BlobCache.Secure.InsertObject(key, obj);
                    break;
                case CacheType.Memory:
                    await BlobCache.InMemory.InsertObject(key, obj);
                    break;
            }
        }

        public async Task<byte[]> RecoverOrFetchImageAsync(string url, CacheType source)
        {
            try
            {
                byte[] cacheObject;

                switch (source)
                {
                    case CacheType.Local:
                        cacheObject = await BlobCache.LocalMachine.DownloadUrl(url);
                        break;
                    case CacheType.User:
                        cacheObject = await BlobCache.UserAccount.DownloadUrl(url);
                        break;
                    case CacheType.Secure:
                        cacheObject = await BlobCache.Secure.DownloadUrl(url);
                        break;
                    case CacheType.Memory:
                        cacheObject = await BlobCache.InMemory.DownloadUrl(url);
                        break;
                    default:
                        cacheObject = default;
                        break;
                }

                return cacheObject;
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }
    }
}
