using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C3Feature
{
	internal class ObjectAndCollection
	{
		public void Run()
		{
			Cat cat = new Cat { Age = 10, Name = "Fluffy" };
			Cat sameCat = new Cat("Fluffy") { Age = 10 };

			var identity = new Matrix
			{
				[0, 0] = 1.0,
				[0, 1] = 0.0,
				[0, 2] = 0.0,

				//[1, 0] = 0.0,
				//[1, 1] = 1.0,
				//[1, 2] = 0.0,

				[2, 0] = 0.0,
				[2, 1] = 0.0,
				[2, 2] = 1.0,
			};

			Console.WriteLine(identity[2, 2]);


			var thing = new IndexersExample
			{
				name = "object one",
				[1] = '1',
				[2] = '4',
				[3] = '9',
				Size = Math.PI,
				['C', 4] = "Middle C"
			};


			//Object Initializers with anonymous types
			var pet = new { Age = 10, Name = "Fluffy" };

			//Object Initializers with the required modifier
			//var pet2 = new Pet() { Age = 10 };


			//Object Initializers with the init accessor
			// public string LastName { get; init; }

			var numbers = new Dictionary<int, string>
			{
				[7] = "seven",
				[9] = "nine",
				[13] = "thirteen"
			};

			//
			FormattedAddresses addresses = new FormattedAddresses()
			{
				{"John", "Doe", "123 Street", "Topeka", "KS", "00000" },
				{"Jane", "Smith", "456 Street", "Topeka", "KS", "00000" }
			};


			Console.WriteLine("Address Entries:");

			foreach (string addressEntry in addresses)
			{
				Console.WriteLine("\r\n" + addressEntry);
			}

			//DictionaryExample
			RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary1
			= new RudimentaryMultiValuedDictionary<string, string>()
			{
				{"Group1", "Bob", "John", "Mary" },
				{"Group2", "Eric", "Emily", "Debbie", "Jesse" }
			};

			RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary2
				= new RudimentaryMultiValuedDictionary<string, string>()
				{
					["Group1"] = new List<string>() { "Bob", "John", "Mary" },
					["Group2"] = new List<string>() { "Eric", "Emily", "Debbie", "Jesse" }
				};
			RudimentaryMultiValuedDictionary<string, string> rudimentaryMultiValuedDictionary3
				= new RudimentaryMultiValuedDictionary<string, string>()
				{
				{"Group1", new string []{ "Bob", "John", "Mary" } },
				{ "Group2", new string[]{ "Eric", "Emily", "Debbie", "Jesse" } }
				};

			Console.WriteLine("Using first multi-valued dictionary created with a collection initializer:");

			foreach (KeyValuePair<string, List<string>> group in rudimentaryMultiValuedDictionary1)
			{
				Console.WriteLine($"\r\nMembers of group {group.Key}: ");

				foreach (string member in group.Value)
				{
					Console.WriteLine(member);
				}
			}

			Console.WriteLine("\r\nUsing second multi-valued dictionary created with a collection initializer using indexing:");

			foreach (KeyValuePair<string, List<string>> group in rudimentaryMultiValuedDictionary2)
			{
				Console.WriteLine($"\r\nMembers of group {group.Key}: ");

				foreach (string member in group.Value)
				{
					Console.WriteLine(member);
				}
			}
			Console.WriteLine("\r\nUsing third multi-valued dictionary created with a collection initializer using indexing:");

			foreach (KeyValuePair<string, List<string>> group in rudimentaryMultiValuedDictionary3)
			{
				Console.WriteLine($"\r\nMembers of group {group.Key}: ");

				foreach (string member in group.Value)
				{
					Console.WriteLine(member);
				}
			}


		}
	}

	class FormattedAddresses : IEnumerable<string>
	{
		private List<string> internalList = new List<string>();
		public IEnumerator<string> GetEnumerator() => internalList.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => internalList.GetEnumerator();

		public void Add(string firstname, string lastname,
			string street, string city,
			string state, string zipcode) => internalList.Add(
			$@"{firstname} {lastname}
{street}
{city}, {state} {zipcode}"
			);
	}

	internal class Cat
	{
		// Auto-implemented properties.
		public int Age { get; set; }
		public string? Name { get; set; }

		public Cat()
		{
		}

		public Cat(string name)
		{
			this.Name = name;
		}
	}

	public class Matrix
	{
		private double[,] storage = new double[3, 3];

		public double this[int row, int column]
		{
			// The embedded array will throw out of range exceptions as appropriate.
			get { return storage[row, column]; }
			set { storage[row, column] = value; }
		}
	}

	internal class IndexersExample
	{
		public string name { get; set; }
		private int[] position = new int[3];
		public double Size { get; set; }
		//private string[,] strings = new string[char, 3];

		public int this[int index]
		{
			get { return position[index]; }
			set { position[index] = value; }
		}

		//public string this[char c, int i] {  }

		public string this[char index1, int index2]
		{
			get
			{
				if (index1 == 'C' && index2 == 4)
					return "Middle C";
				else
					throw new ArgumentOutOfRangeException("index", "Indexers must be 'C' and 4");
			}
			set
			{
				if (index1 == 'C' && index2 == 4)
					Console.WriteLine("Middle C set to: " + value);
				else
					throw new ArgumentOutOfRangeException("index", "Indexers must be 'C' and 4");
			}
		}

	}

	//public class Pet
	//{
	//	public required int Age;
	//	public string Name;

	//	public Pet(int age, string name)
	//	{
	//		Age = age;
	//		Name = name;
	//	}
	//}

	class RudimentaryMultiValuedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, List<TValue>>> where TKey : notnull
	{
		private Dictionary<TKey, List<TValue>> internalDictionary = new Dictionary<TKey, List<TValue>>();

		public IEnumerator<KeyValuePair<TKey, List<TValue>>> GetEnumerator() => internalDictionary.GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => internalDictionary.GetEnumerator();

		public List<TValue> this[TKey key]
		{
			get => internalDictionary[key];
			set => Add(key, value);
		}

		public void Add(TKey key, params TValue[] values) => Add(key, (IEnumerable<TValue>)values);

		public void Add(TKey key, IEnumerable<TValue> values)
		{
			if (!internalDictionary.TryGetValue(key, out List<TValue>? storedValues))
				internalDictionary.Add(key, storedValues = new List<TValue>());

			storedValues.AddRange(values);
		}
	}

}
