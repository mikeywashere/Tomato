using System;
using System.Collections.Concurrent;

namespace Tomato.Repository
{
    /// <summary>
    /// PropertyRepository stores "properties" for a guid. I used guid because it was easy in this instance.
    /// </summary>
    public class PropertyRepository : IPropertyRepository
    {
        private InMemoryRepository<Guid, ConcurrentDictionary<string, object>> propertyStore = new InMemoryRepository<Guid, ConcurrentDictionary<string, object>>();

        public T Get<T>(Guid key, string name) where T : class
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var properties = propertyStore.Get(key);

            if (properties == null)
                return null;

            if (properties.TryGetValue(name, out object value))
            {
                return value as T;
            }

            return null;
        }

        public void Put<T>(Guid key, string name, T value) where T : class
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var properties = propertyStore.Get(key);

            if (properties == null)
            {
                properties = new ConcurrentDictionary<string, object>();
                propertyStore.Put(key, properties);
                properties.TryAdd(name, value);
                return;
            }

            properties.AddOrUpdate(name, value, (n, v) => value);
        }
    }
}