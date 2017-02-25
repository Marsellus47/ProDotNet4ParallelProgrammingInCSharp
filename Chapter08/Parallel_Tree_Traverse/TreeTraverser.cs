using System;
using System.Threading.Tasks;

namespace Parallel_Tree_Traverse
{
	public class TreeTraverser
	{
		public static void TraverseeTree<T>(Tree<T> tree, Action<T> action)
		{
			if(tree != null)
			{
				// Invoke the action for the data
				action.Invoke(tree.Data);

				// Start tasks to process the left and right nodes if they exist
				if(tree.LeftNode != null && tree.RightNode != null)
				{
					Task leftTask = Task.Factory.StartNew(() => TraverseeTree(tree.LeftNode, action));
					Task rightTask = Task.Factory.StartNew(() => TraverseeTree(tree.RightNode, action));

					// Wait for the tasks to complete
					Task.WaitAll(leftTask, rightTask);
				}
			}
		}
	}
}
