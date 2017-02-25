using System;

namespace Parallel_Tree_Traverse
{
	class Program
	{
		static void Main(string[] args)
		{
			// Create and populate simple tree
			Tree<int> tree = PopulateTree(new Tree<int>(), new Random());

			// Traverse the tree, print out the event values
			TreeTraverser.TraverseeTree(tree, item =>
			{
				if(item % 2 == 0)
				{
					Console.WriteLine("Item {0}", item);
				}
			});

			// Wait for input before exiting
			Console.WriteLine("Press enter to finish");
			Console.ReadLine();
		}

		private static Tree<int> PopulateTree(Tree<int> parentNode, Random random, int depth = 0)
		{
			parentNode.Data = random.Next(1, 1000);
			if(depth < 10)
			{
				parentNode.LeftNode = new Tree<int>();
				parentNode.RightNode = new Tree<int>();
				PopulateTree(parentNode.LeftNode, random, depth + 1);
				PopulateTree(parentNode.RightNode, random, depth + 1);
			}
			return parentNode;
		}
	}
}
