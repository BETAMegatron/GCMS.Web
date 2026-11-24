using Microsoft.Extensions.Options;
using StackExchange.Redis;
namespace GCMS.Web.Services.Redis
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;
        public RedisService(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<string?> GetAsync(string key)
        {
            RedisValue value = await _database.StringGetAsync(key);
            if (value.IsNull)
                return null;
            return value.ToString();
        }

        public async Task RemoveAsync(string key)
        {
            var value = await _database.KeyDeleteAsync(key);
        }

        public Task SetAsync(string key, string value)
        {
            return  _database.StringSetAsync(key, value);
        }
    }
}
