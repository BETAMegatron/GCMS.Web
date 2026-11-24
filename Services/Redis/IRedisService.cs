using StackExchange.Redis;
namespace GCMS.Web.Services.Redis
{
    public interface IRedisService
    {
        public Task SetAsync(string key, string value);

        public Task<string?> GetAsync(string key);

        public Task RemoveAsync(string key);

        public Task<bool> ExistsAsync(string key);
    }
}