using DotnetPractice.C3Feature;
using DotnetPractice.C4Feature;
using DotnetPractice.C5Feature;

namespace DotnetPractice
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Practice and check c# feature");
			//https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-80
			//new SpanPractice().Do();

			//new C2Feature();

			//new C3Features();

			//new C4Features().Run();

			//Task.Run(()=> MainAsync(args));
			MainAsync(args).Wait();

			//new C7Feature();

			//new C8Feature();

			//new OperatorOverloading().OnOperatorOverloading();
		}

		static async Task MainAsync(string[] args)
		{
			try
			{
				await new C5Features().Run();

				//await Task.Yield();
				//Thread.Sleep(5000);

				// Asynchronous implementation.
				// await Task.Delay(1000);
			}
			catch (Exception ex)
			{
				// Handle exceptions.
			}
		}

	}

	

	internal class People
	{
        public int Age { get; set; }
        public string Name { get; set; }
    }



}