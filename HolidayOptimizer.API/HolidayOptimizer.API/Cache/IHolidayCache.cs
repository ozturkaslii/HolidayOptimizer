using System;
using System.Threading.Tasks;

namespace HolidayOptimizer.API.Cache
{
    public interface IHolidayCache
    {
        Task<T> GetOrCreateCacheAsync<T>(Func<T> method, string cacheKey)
            where T : class;
    }
}