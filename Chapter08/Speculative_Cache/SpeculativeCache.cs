using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Speculative_Cache
{
	public class SpeculativeCache<TKey, TValue>
	{
		private ConcurrentDictionary<TKey, Lazy<TValue>> dictionary;
		private BlockingCollection<TKey> queue;
		private Func<TKey, TKey[]> speculatorFunction;
		private Func<TKey, TValue> factoryFunction;

		public SpeculativeCache(Func<TKey, TValue> factory,
			Func<TKey, TKey[]> speculator)
		{
			speculatorFunction = speculator;
			dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
			queue = new BlockingCollection<TKey>();

			// Create the wrapper function
			factoryFunction = key =>
			{
				// Call the factory function
				TValue value = factory(key);

				// Add the key to the speculative queue
				queue.Add(key);

				// Return the results
				return value;
			};

			// Start the task that will handle speculation
			Task.Factory.StartNew(() =>
			{
				Parallel.ForEach(queue.GetConsumingEnumerable(),
					new ParallelOptions { MaxDegreeOfParallelism = 2 },
					key =>
					{
						// Enumerate the keys to speculate
						foreach (var specKey in speculatorFunction(key))
						{
							TValue res = dictionary.GetOrAdd(specKey, new Lazy<TValue>(() => factory(specKey))).Value;
						}
					});
			});
		}

		public TValue GetValue(TKey key)
		{
			return dictionary.GetOrAdd(key, new Lazy<TValue>(() => factoryFunction(key))).Value;
		}
	}
}
