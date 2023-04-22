using DecoratorPatternCachingExample.Interfaces;
using DecoratorPatternCachingExample.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorPatternCachingExample.Persistence.Caching
{
    public class StudentCachingDecorator<T> : IStudentRepository
        where T : IStudentRepository
    {
        private readonly T _model;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        private readonly string _storeKey = "student_store_cache_key";
        private readonly double _cachedDataTimeExpirationInSeconds;

        public StudentCachingDecorator(T model,
            IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _model = model;
            _configuration = configuration;

            var cachedDataTimeExpirationStr = _configuration.GetValue<string>("CachedDataTimeExpirationInSeconds");
            if (cachedDataTimeExpirationStr != null)
            {
                _cachedDataTimeExpirationInSeconds = Double.Parse(cachedDataTimeExpirationStr);
            }
            else
            {
                throw new Exception("Config [CachedDataTimeExpirationInSeconds] required");
            }
        }

        public async Task<List<Student>> ListAsync()
        {
            var listStore = new List<Student>();
            var storeCache = _memoryCache.Get(_storeKey);

            if (storeCache == null)
            {
                var students = await _model.ListAsync();
                if (students != null)
                {
                    _memoryCache.Set(_storeKey, students, TimeSpan.FromSeconds(_cachedDataTimeExpirationInSeconds));
                    listStore = students;

                    GlobalInfo.IsDataFromCache = false; // This snippet is only to help our DEMO
                }
            }
            else
            {
                listStore = (List<Student>)storeCache;

                GlobalInfo.IsDataFromCache = true; // This snippet is only to help our DEMO
            }

            return listStore;
        }
    }
}
