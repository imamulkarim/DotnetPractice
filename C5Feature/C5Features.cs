using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C5Feature
{
	internal class C5Features
	{
		public async Task Run()
		{
			//Asynchronous programming with async and await
			await new AsyncBreakfast().Build();

			//await ThrowExceptionAsync();

			//Configure Context
			//https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming#configure-context
			await MyMethodAsync();

			await KnowYourTools();
		}

		private async Task KnowYourTools()
		{
			//https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming#know-your-tools


			//1. Create a task to execute code	Task.Run or TaskFactory.StartNew (not the Task constructor or Task.Start)
			//https://devblogs.microsoft.com/pfxteam/task-run-vs-task-factory-startnew/
			//Support cancellation	CancellationTokenSource and CancellationToken
			//https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-8.0
			await CancellationToken();
		}

		private async Task CancellationToken()
		{
			// Define the cancellation token.
			CancellationTokenSource source = new CancellationTokenSource();
			CancellationToken token = source.Token;

			Random rnd = new Random();
			Object lockObj = new Object();

			List<Task<int[]>> tasks = new List<Task<int[]>>();
			TaskFactory factory = new TaskFactory(token);
			for (int taskCtr = 0; taskCtr <= 10; taskCtr++)
			{
				int iteration = taskCtr + 1;
				tasks.Add(factory.StartNew(() => {
					int value;
					int[] values = new int[10];
					for (int ctr = 1; ctr <= 10; ctr++)
					{
						lock (lockObj)
						{
							value = rnd.Next(0, 101);
						}
						if (value == 0)
						{
							source.Cancel();
							Console.WriteLine("Cancelling at task {0}", iteration);
							break;
						}
						values[ctr - 1] = value;
					}
					return values;
				}, token));
			}
			try
			{
				Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),
				(results) => {
					Console.WriteLine("Calculating overall mean...");
					long sum = 0;
					int n = 0;
					foreach (var t in results)
					{
						foreach (var r in t.Result)
						{
							sum += r;
							n++;
						}
					}
					return sum / (double)n;
				}, token);
				Console.WriteLine("The mean is {0}.", fTask.Result);
			}
			catch (AggregateException ae)
			{
				foreach (Exception e in ae.InnerExceptions)
				{
					if (e is TaskCanceledException)
						Console.WriteLine("Unable to compute mean: {0}",
						   ((TaskCanceledException)e).Message);
					else
						Console.WriteLine("Exception: " + e.GetType().Name);
				}
			}
			finally
			{
				source.Dispose();
			}
		}

		async Task MyMethodAsync()
		{
			// Code here runs in the original context.
			await Task.Delay(1000);
			// Code here runs in the original context.
			await Task.Delay(1000).ConfigureAwait(
			  continueOnCapturedContext: false);
			// Code here runs without the original
			// context (in this case, on the thread pool).
		}

		private async Task ThrowExceptionAsync()
		{
			throw new InvalidOperationException();
		}
		public void AsyncVoidExceptions_CannotBeCaughtByCatch()
		{
			try
			{
				ThrowExceptionAsync();
			}
			catch (Exception)
			{
				Console.WriteLine("exception caught");
				// The exception is never caught here!
				throw;
			}
		}
	}
}
