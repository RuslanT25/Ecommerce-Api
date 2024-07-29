using Ecommerce.Application.Interfaces.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure.Services.RedisCache;

public class RedisCacheService : IRedisCatcheService
{
    private readonly ConnectionMultiplexer _redisConnection;
    private readonly IDatabase _database;
    private readonly RedisOptions _redisOptions;
    public RedisCacheService(IOptions<RedisOptions> options)
    {
        _redisOptions = options.Value;
        var opt = ConfigurationOptions.Parse(_redisOptions.ConnectionString);
        _redisConnection = ConnectionMultiplexer.Connect(opt);
        _database = _redisConnection.GetDatabase();
    }
    public async Task<T> GeTAsync<T>(string key)
    {
        RedisValue value = await _database.StringGetAsync(key);
        if (value.HasValue)
            return JsonConvert.DeserializeObject<T>(value!)!;

        return default!;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? expirationDate = null)
    {
        TimeSpan timeUntilExpration = expirationDate!.Value - DateTime.Now;
        await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), timeUntilExpration);
    }
}
