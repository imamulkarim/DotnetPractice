namespace DotnetPractice
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");

			new SpanPractice().Do();
		}
	}

	internal class People
	{
        public int Age { get; set; }
        public string Name { get; set; }
    }
}