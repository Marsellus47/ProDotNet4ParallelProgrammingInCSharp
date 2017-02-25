using System;
using System.Collections.Concurrent;

namespace Parallel_Cache
{
	public class ParallelCache<TKey, TValue>
	{
		private ConcurrentDictionary<TKey, Lazy<TValue>> dictionary;
		private Func<TKey, TValue> valueFactory;

		public ParallelCache(Func<TKey, TValue> factory)
		{
			// Set the factory instance variable
			valueFactory = factory;

			// Initialize the dictionary
			dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
		}

		public TValue GetValue(TKey key)
		{
			return dictionary.GetOrAdd(key, new Lazy<TValue>(() => valueFactory(key))).Value;
		}
	}
}
