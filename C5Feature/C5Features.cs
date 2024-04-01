using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C5Feature
{
	internal class C5Features
	{
		public async Task Run()
		{
			//Asynchronous programming with async and await
			//await new AsyncBreakfast().Build();

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
			var t = Task.Factory.StartNew(() =>
			{
				Task<int> inner = Task.Factory.StartNew(() => 42);
				return inner;
			});

			//same as previous Task<Task<int>>

			var t2 = Task.Factory.StartNew(async delegate
			{
				await Task.Delay(1000);
				return 42;
			});


			var t3 = Task.Factory.StartNew(async delegate
			{
				await Task.Delay(1000);
				return 42;
			}).Unwrap(); // .NET 4 we introduced the Unwrap method. 

			var t4 = Task.Run(async delegate
			{
				await Task.Delay(1000);
				return 42;
			}); //same as prevoius

			var t5 = Task.Factory.StartNew(async delegate
			{
				await Task.Delay(1000);
				return 42;
			}, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default).Unwrap();

			int result = await Task.Run(async () =>
			{
				await Task.Delay(1000);
				return 42;
			});

			//2. TaskFactory.FromAsync or TaskCompletionSource<T>
			//Create a task wrapper for an operation or event
			//https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1?view=net-8.0
			var TaskCompletionSourceMethod = () => {
				
				Console.WriteLine("*******************TaskCompletionSourceMethod*********************");
				TaskCompletionSource<int> tcs1 = new TaskCompletionSource<int>();
				Task<int> t1 = tcs1.Task;

				// Start a background task that will complete tcs1.Task
				Task.Factory.StartNew(() =>
				{
					Thread.Sleep(1000);
					tcs1.SetResult(15);
				});

				// The attempt to get the result of t1 blocks the current thread until the completion source gets signaled.
				// It should be a wait of ~1000 ms.
				Stopwatch sw = Stopwatch.StartNew();
				int result = t1.Result;
				sw.Stop();

				Console.WriteLine("(ElapsedTime={0}): t1.Result={1} (expected 15) ", sw.ElapsedMilliseconds, result);

				// ------------------------------------------------------------------

				// Alternatively, an exception can be manually set on a TaskCompletionSource.Task
				TaskCompletionSource<int> tcs2 = new TaskCompletionSource<int>();
				Task<int> t2 = tcs2.Task;

				// Start a background Task that will complete tcs2.Task with an exception
				Task.Factory.StartNew(() =>
				{
					Thread.Sleep(1000);
					tcs2.SetException(new InvalidOperationException("SIMULATED EXCEPTION"));
				});

				// The attempt to get the result of t2 blocks the current thread until the completion source gets signaled with either a result or an exception.
				// In either case it should be a wait of ~1000 ms.
				sw = Stopwatch.StartNew();
				try
				{
					result = t2.Result;

					Console.WriteLine("t2.Result succeeded. THIS WAS NOT EXPECTED.");
				}
				catch (AggregateException e)
				{
					Console.Write("(ElapsedTime={0}): ", sw.ElapsedMilliseconds);
					Console.WriteLine("The following exceptions have been thrown by t2.Result: (THIS WAS EXPECTED)");
					for (int j = 0; j < e.InnerExceptions.Count; j++)
					{
						Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
					}
				}
			};
			//TaskCompletionSourceMethod();

			var TaskFromAsync = () =>
			{
				static async Task<string> ReadFileAsync(string filePath)
				{
					if (!File.Exists(filePath)) return null;

					using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					{
						var buffer = new byte[fs.Length];

						await Task.Factory.FromAsync(fs.BeginRead, fs.EndRead, buffer, 0, buffer.Length, TaskCreationOptions.None);

						using (var ms = new MemoryStream(buffer))
						{
							using (var sr = new StreamReader(ms))
							{
								return sr.ReadToEnd();
							}
						}
					}
				}
			};
			//TaskFromAsync();


			//3. Support cancellation	CancellationTokenSource and CancellationToken
			var CancellationTokenMethod = () => {
				//https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken?view=net-8.0

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

			};
			CancellationTokenMethod();

			var CancellationTokenSourceMethod = () => {
				//https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource?view=net-8.0
			};
			CancellationTokenSourceMethod();

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
