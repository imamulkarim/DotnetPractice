using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C5Feature
{
	internal class AsyncBreakfast
	{
		public async Task Build()
		{

			Task finishedTask = null;
			try
			{
				Coffee cup = PourCoffee();
				Console.WriteLine(DateTimeOffset.Now);
				Console.WriteLine("coffee is ready");

				Task<Egg> eggsTask = FryEggsAsync(2);
				//var eggs = await eggsTask;
				//Console.WriteLine("eggs are ready");

				//Bacon bacon = FryBacon(3);
				Task<Bacon> baconTask = FryBaconAsync(3);
				//var bacon = await baconTask;
				//Console.WriteLine("bacon is ready");

				//Toast toast = ToastBread(2);
				Task<Toast> toastTask = MakeToastWithButterAndJamAsync(2);

				//var toast = await toastTask;
				//ApplyButter(toast);
				//ApplyJam(toast);
				//Console.WriteLine("toast is ready");

				//await Task.WhenAll(eggsTask, baconTask, toastTask);
				//Console.WriteLine("Eggs are ready");
				//Console.WriteLine("Bacon is ready");
				//Console.WriteLine("Toast is ready");

				var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
				while (breakfastTasks.Count > 0)
				{
					finishedTask = await Task.WhenAny(breakfastTasks);
					if (finishedTask == eggsTask)
					{
						Console.WriteLine("Eggs are ready");
					}
					else if (finishedTask == baconTask)
					{
						Console.WriteLine("Bacon is ready");
					}
					else if (finishedTask == toastTask)
					{
						Console.WriteLine("Toast is ready");
					}
					await finishedTask;
					breakfastTasks.Remove(finishedTask);
				}


				Juice oj = PourOJ();
				Console.WriteLine("oj is ready");
				Console.WriteLine("Breakfast is ready!");
				Console.WriteLine(DateTimeOffset.Now);
			}
			catch (Exception)
			{
				AggregateException allExceptions = finishedTask.Exception;
				//https://www.codeguru.com/csharp/async-methods-exception-handling/
				foreach (var ex in allExceptions.InnerExceptions)
				{
					Console.WriteLine(ex.GetType().ToString());
				}
				throw;
			}
		}


		private static Juice PourOJ()
		{
			Console.WriteLine("Pouring orange juice");
			return new Juice();
		}

		private static void ApplyJam(Toast toast) =>
			Console.WriteLine("Putting jam on the toast");

		private static void ApplyButter(Toast toast) =>
			Console.WriteLine("Putting butter on the toast");

		private static Toast ToastBread(int slices)
		{
			for (int slice = 0; slice < slices; slice++)
			{
				Console.WriteLine("Putting a slice of bread in the toaster");
			}
			Console.WriteLine("Start toasting...");
			Task.Delay(3000).Wait();
			Console.WriteLine("Remove toast from toaster");

			return new Toast();
		}

		static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
		{
			var toast = await ToastBreadAsync(number);
			ApplyButter(toast);
			ApplyJam(toast);

			return toast;
		}

		private static async Task<Toast> ToastBreadAsync(int slices)
		{
			for (int slice = 0; slice < slices; slice++)
			{
				Console.WriteLine("Putting a slice of bread in the toaster");
			}
			Console.WriteLine("Start toasting...");
			await Task.Delay(2000);
			//Console.WriteLine("Fire! Toast is ruined!");
			//throw new InvalidOperationException("The toaster is on fire");
			await Task.Delay(1000);
			Console.WriteLine("Remove toast from toaster");

			return new Toast();
		}

		private static Bacon FryBacon(int slices)
		{
			Console.WriteLine($"putting {slices} slices of bacon in the pan");
			Console.WriteLine("cooking first side of bacon...");
			Task.Delay(3000).Wait();
			for (int slice = 0; slice < slices; slice++)
			{
				Console.WriteLine("flipping a slice of bacon");
			}
			Console.WriteLine("cooking the second side of bacon...");
			Task.Delay(3000).Wait();
			Console.WriteLine("Put bacon on plate");

			return new Bacon();
		}

		private async Task<Bacon> FryBaconAsync(int slices)
		{
			Console.WriteLine($"putting {slices} slices of bacon in the pan");
			Console.WriteLine("cooking first side of bacon...");
			await Task.Delay(3000);
			for (int slice = 0; slice < slices; slice++)
			{
				Console.WriteLine("flipping a slice of bacon");
			}
			Console.WriteLine("cooking the second side of bacon...");
			await Task.Delay(3000);
			Console.WriteLine("Put bacon on plate");

			return new Bacon();
		}

		private static Egg FryEggs(int howMany)
		{
			Console.WriteLine("Warming the egg pan...");
			Task.Delay(3000).Wait();
			Console.WriteLine($"cracking {howMany} eggs");
			Console.WriteLine("cooking the eggs ...");
			Task.Delay(3000).Wait();
			Console.WriteLine("Put eggs on plate");

			return new Egg();
		}

		private async Task<Egg> FryEggsAsync(int howMany)
		{
			Console.WriteLine("Warming the egg pan...");
			await Task.Delay(3000);
			Console.WriteLine($"cracking {howMany} eggs");
			Console.WriteLine("cooking the eggs ...");
			await Task.Delay(3000);
			Console.WriteLine("Put eggs on plate");

			return new Egg();
		}

		private static Coffee PourCoffee()
		{
			Console.WriteLine("Pouring coffee");
			return new Coffee();
		}


	}

	internal class Bacon { }
	internal class Coffee { }
	internal class Egg { }
	internal class Juice { }
	internal class Toast { }
}
