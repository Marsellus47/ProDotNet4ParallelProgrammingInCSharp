using System.Threading;

namespace Listing26
{
	public class WorkItem
	{
		public int WorkDuration { get; set; }

		public void PerformWork()
		{
			// Simulate work by sleeping
			Thread.Sleep(WorkDuration);
		}
	}
}
