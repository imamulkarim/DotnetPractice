using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C5Feature
{
	internal class DeadlockDemo
	{
		//https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
		private static async Task DelayAsync()
		{
			await Task.Delay(1000);
		}
		// This method causes a deadlock when called in a GUI or ASP.NET context.
		public static void Test()
		{
			// Start the delay.
			var delayTask = DelayAsync();
			// Wait for the delay to complete.
			delayTask.Wait();
		}

		/*
			The root cause of this deadlock is due to the way await handles contexts. By default, 
			when an incomplete Task is awaited, the current “context” is captured and used to resume the method when the Task completes. 
			This “context” is the current SynchronizationContext unless it’s null, in which case it’s the current TaskScheduler. 
			GUI and ASP.NET applications have a SynchronizationContext that permits only one chunk of code to run at a time. 
			When the await completes, it attempts to execute the remainder of the async method within the captured context. 
			But that context already has a thread in it, which is (synchronously) waiting for the async method to complete. 
			They’re each waiting for the other, causing a deadlock.
		 */
	}
}
