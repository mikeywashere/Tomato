using System;
using System.Dynamic;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Tomato.Interface;

namespace Tomato.Repository
{
    public class InMemoryRepository<TKey, TVal> : IRepository<TKey, TVal>
    {
        private ConcurrentDictionary<TKey, TVal> data = new ConcurrentDictionary<TKey, TVal>();

        public TVal Get(TKey key)
        {
            if (data.TryGetValue(key, out TVal value))
            {
                return value;
            }
            return default(TVal);
        }

        public void Put(TKey key, TVal value)
        {
            data.AddOrUpdate(key, value, (k, v) => value);
        }
    }
}
