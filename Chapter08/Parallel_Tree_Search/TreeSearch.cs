using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Tree_Search
{
	public class TreeSearch
	{
		public static T SearchTree<T>(Tree<T> tree, Func<T, bool> searchFunction)
		{
			// Create the cancellation token source
			CancellationTokenSource tokenSource = new CancellationTokenSource();

			// Search the tree
			TWrapper<T> result = PerformSearch(tree, searchFunction, tokenSource);
			return result == null ? default(T) : result.Value;
		}

		private static TWrapper<T> PerformSearch<T>(
			Tree<T> tree,
			Func<T, bool> searchFunction,
			CancellationTokenSource tokenSource)
		{
			// Define the result
			TWrapper<T> result = null;

			// Only proceed if we have something to search
			if(tree != null)
			{
				// Apply the search function to the current tree
				if (searchFunction(tree.Data))
				{
					// Cancel the token source
					tokenSource.Cancel();

					// Set the result
					result = new TWrapper<T> { Value = tree.Data };
				}
				else
				{
					// We have not found a result - continue the search
					if (tree.LeftNode != null && tree.RightNode != null)
					{
						// Start the task for the left node
						Task<TWrapper<T>> leftTask = Task<TWrapper<T>>.Factory.StartNew(
							() => PerformSearch(tree.LeftNode, searchFunction, tokenSource),
							tokenSource.Token);

						// Start the task for the right node
						Task<TWrapper<T>> rightTask = Task<TWrapper<T>>.Factory.StartNew(
							() => PerformSearch(tree.RightNode, searchFunction, tokenSource),
							tokenSource.Token);

						try
						{
							// Set the result based on the tasks
							result = leftTask.Result != null
								? leftTask.Result
								: rightTask != null
									? rightTask.Result
									: null;
						}
						catch (AggregateException) { }
					}
				}
			}

			// Return the result
			return result;
		}
	}
}
