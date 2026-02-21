namespace Homework2_sem2.Interfaces;

public interface ICacheService
{
    // TTL сделаем дефолтным
    Task<T?> GetOrSetAsync<T>(string redisKey, string sqlQuery);
}