using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace HolidayOptimizer.API.Cache
{
    public class HolidayCache : IHolidayCache
    {
        private readonly IMemoryCache _cache;

        public HolidayCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetOrCreateCacheAsync<T>(Func<T> method, string cacheKey) 
            where T : class
        {
            return await _cache.GetOrCreateAsync(cacheKey, entry =>
            {
                entry.SlidingExpiration = new TimeSpan(0, 1, 0);
                return Task.FromResult(method());
            });
        }
    }
}