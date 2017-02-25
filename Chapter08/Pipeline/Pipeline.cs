using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Pipeline
{
	public class Pipeline<TInput, TOutput>
	{
		// Queue based blocking collection
		private BlockingCollection<ValueCallbackWrapper> valueQueue;

		// The function to use
		Func<TInput, TOutput> pipelineFunction;

		public Pipeline(Func<TInput, TOutput> function)
		{
			pipelineFunction = function;
		}

		public Pipeline<TInput, TNewOutput> AddFunction<TNewOutput>(Func<TOutput, TNewOutput> newFunction)
		{
			// Create a composite function
			Func<TInput, TNewOutput> compositeFunction = inputValue => newFunction(pipelineFunction(inputValue));

			// Return a new pipeline around the composite function
			return new Pipeline<TInput, TNewOutput>(compositeFunction);
		}

		public void AddValue(TInput value, Action<TInput, TOutput> callback)
		{
			// Add the value to the queue for processing
			valueQueue.Add(new ValueCallbackWrapper { Value = value, Callback = callback });
		}

		public void StartProcessing()
		{
			// Initialize the collection
			valueQueue = new BlockingCollection<ValueCallbackWrapper>();

			// Create a parallel loop to consume items from the collection
			Task.Factory.StartNew(() =>
			{
				Parallel.ForEach(
					valueQueue.GetConsumingEnumerable(),
					wrapper =>
					{
						wrapper.Callback(wrapper.Value, pipelineFunction(wrapper.Value));
					});
			});
		}

		public void StopProcessing()
		{
			// Signal to the collection that no further values will be added
			valueQueue.CompleteAdding();
		}

		private class ValueCallbackWrapper
		{
			public TInput Value;
			public Action<TInput, TOutput> Callback;
		}
	}
}
