using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookStore.Modules
{
    public static class RedisExtensions
    {
        public static IDatabase GetRedis(this IConnectionMultiplexer connectionMultiplexer)
        {
            return connectionMultiplexer.GetDatabase();
        }

        public static async Task SetRecordAsync<T>(this IDatabase redisDatabase,
                                                string recordId,
                                                T data,
                                                TimeSpan? expireTime = null)
        {
            //expireTime = expireTime ?? TimeSpan.FromHours(3);
            expireTime = expireTime ?? TimeSpan.FromSeconds(30);

            var serializedData = JsonSerializer.Serialize(data);

            await redisDatabase.StringSetAsync(recordId, serializedData, expireTime);
        }

        public static async Task<T> GetRecordAsync<T>(this IDatabase redisDatabase, string recordId)
        {
            var serializedData = await redisDatabase.StringGetAsync(recordId);

            if (serializedData.IsNullOrEmpty)
            {
                return default;
            }

            T deserializedData = JsonSerializer.Deserialize<T>(serializedData);
            return deserializedData;
        }
    }
}
