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
			
			
			//1. Create a task to execute code	Task.		Run or TaskFactory.StartNew (not the Task constructor or Task.Start)
				//https://devblogs.microsoft.com/pfxteam/task-run-vs-task-factory-startnew/


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
