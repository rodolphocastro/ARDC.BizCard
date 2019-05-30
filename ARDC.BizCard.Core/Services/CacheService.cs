using Akavache;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public class CacheService : ICacheService
    {
        public CacheService()
        {
            Akavache.Registrations.Start("ArdcBizCard");    // Inicializa o Akavache
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
    }
}
