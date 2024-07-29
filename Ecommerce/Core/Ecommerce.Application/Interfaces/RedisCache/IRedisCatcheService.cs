namespace Ecommerce.Application.Interfaces.RedisCache;

public interface IRedisCatcheService
{
    Task<T> GeTAsync<T>(string key);
    Task SetAsync<T>(string key, T value, DateTime? expirationDate = null);
}
