using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotnetPractice
{
	internal class C7Feature
	{
		public C7Feature() {

			//https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/

			//Is-expressions with patterns
			Console.WriteLine("Is-expressions with patterns");
			PrintStars(3);


			//Switch statements with patterns

			//tuples
			Console.WriteLine("tuples");
			var names = LookupName(3);
			Console.WriteLine($"found {names.first} {names.Item3}.");

			//Deconstruction
			Console.WriteLine("A deconstructing declaration");
			(string first, var middle, string last) = LookupName(3); // deconstructing declaration
			Console.WriteLine($"found {first} {last}.");

			//Local functions
			Console.WriteLine("Local functions");
			Console.WriteLine(Fibonacci(9));

			IEnumerable<int> localEnum = new List<int> {3 , 4, 5,9,10,33,2,435,78,2 };
			var ff= Filter<int>(localEnum, m=>m>50);
			foreach (var item in ff)
			{
				Console.WriteLine(item);
			}

			//Ref returns and locals
			Console.WriteLine("Ref returns and locals");
			int[] array = { 1, 15, -39, 0, 7, 14, -12 };
			ref int place = ref Find(7, array); // aliases 7's place in the array
			place = 9; // replaces 7 with 9 in the array
			Console.WriteLine(array[4]); // prints 9

			//Generalized async return types
			Console.WriteLine("Generalized async return types");

			//ConcurrentDictionary

			//More expression bodied members
			//But in C# 7.0 we are directly allowing throw as an expression in certain places:
			//public Person(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));



		}

		(string first, string middle, string last) LookupName(long id) // tuple return type
		{
			// retrieve first, middle and last from data storage
			string first = "Fazle ";
			string middle = "Imamul "; 
			 string last = "Karim";
			//return (first, middle, last); // tuple literal
			return (first: first, middle: middle, last: last); // named tuple elements in a literal


		}

		public void PrintStars(object o)
		{
			if (o is null) return;     // constant pattern "null"
			if (!(o is int i)) return; // type pattern "int i"
			System.Console.WriteLine(new string('*', i));
		}

		public int Fibonacci(int x)
		{
			if (x < 0) throw new ArgumentException("Less negativity please!", nameof(x));
			return Fib(x).current;

			(int current, int previous) Fib(int i)
			{
				if (i == 0) return (1, 0);
				var (p, pp) = Fib(i - 1);
				return (p + pp, p);
			}
		}

		public IEnumerable<T> Filter<T>(IEnumerable<T> source, Func<T, bool> filter)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			if (filter == null) throw new ArgumentNullException(nameof(filter));

			return Iterator();

			IEnumerable<T> Iterator()
			{
				foreach (var element in source)
				{
					if (filter(element)) { yield return element; }
				}
			}
		}

		public ref int Find(int number, int[] numbers)
		{
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] == number)
				{
					return ref numbers[i]; // return the storage location, not the value
				}
			}
			throw new IndexOutOfRangeException($"{nameof(number)} not found");
		}

	}
}
