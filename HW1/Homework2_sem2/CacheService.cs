using System.Data;
using System.Text.Json;
using Dapper;
using Homework2_sem2.Interfaces;
using StackExchange.Redis;

namespace Homework2_sem2;

public class CacheService : ICacheService
{
    private readonly StackExchange.Redis.IDatabase _redisDb;
    private readonly IDbConnection _dbConnection;
    private readonly TimeSpan _defaultTtl = TimeSpan.FromMinutes(5);
    private readonly ILogger<CacheService> _logger;

    public CacheService(IConnectionMultiplexer redis, IDbConnection dbConnection, ILogger<CacheService> logger)
    {
        _redisDb = redis.GetDatabase();
        _dbConnection = dbConnection;
        _logger = logger;
    }

    public async Task<T?> GetOrSetAsync<T>(string redisKey, string sqlQuery)
    {
        //пробуем получить данные из Redis
        var cachedValue = await _redisDb.StringGetAsync(redisKey);

        if (!cachedValue.IsNullOrEmpty)
        {
            _logger.LogInformation("Data retrieved from Redis for key: {Key}", redisKey);
            return JsonSerializer.Deserialize<T>(cachedValue!);
        }

        //если в Redis нет, идем в БД
        _logger.LogInformation("Cache miss for key: {Key}. Executing SQL.", redisKey);
        
        var result = await _dbConnection.QueryFirstOrDefaultAsync<T>(sqlQuery);

        if (result != null)
        {
            //прогреваем кэш
            var serialized = JsonSerializer.Serialize(result);
            await _redisDb.StringSetAsync(redisKey, serialized, _defaultTtl);
            
            _logger.LogInformation("Data cached for key: {Key}", redisKey);
        }

        return result;
    }
}