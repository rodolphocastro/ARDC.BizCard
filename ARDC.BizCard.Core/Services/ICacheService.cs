using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public interface ICacheService
    {
        Task InitializeAsync();

        Task StoreObjectAsync(string key, object obj, CacheType destination = CacheType.Memory);

        Task<T> RecoverObjectAsync<T>(string key, CacheType source = CacheType.Memory);

        Task InvalidateObjectAsync(string key, CacheType source = CacheType.Memory);

        Task<byte[]> RecoverOrFetchImageAsync(string url, CacheType source);
    }

    public enum CacheType
    {
        Local,
        User,
        Secure,
        Memory
    }
}
