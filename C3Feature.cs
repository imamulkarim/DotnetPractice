using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice
{
	internal class C3Feature
	{
        public C3Feature()
        {
			//Auto-Implemented Properties
			// Auto-implemented properties for trivial get and set
			//public double TotalPurchases { get; set; }
			//public string FirstName { get; set; } = "Jane";

			//Anonymous types
			var v = new { Amount = 108, Message = "Hello" };
			// Rest the mouse pointer over v.Amount and v.Message in the following
			// statement to verify that their inferred types are int and string.
			Console.WriteLine(v.Amount + v.Message);


			//Query expression basics ****************************************
			// Data source.
			int[] scores = { 90, 71, 82, 93, 75, 82 };

			// Query Expression.
			IEnumerable<int> scoreQuery = //query variable
				from score in scores //required
				where score > 80 // optional
				orderby score descending // optional
				select score; //must end with select or group

			// Execute the query to produce the results
			foreach (int testScore in scoreQuery)
			{
				Console.WriteLine(testScore);
			}
			// Output: 93 90 82 82


			//Lambda expressions and anonymous functions
			//https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions

			//1. Expression lambda   eg.  (input-parameters) => expression
			//Any lambda expression can be converted to a delegate type.  (c2 feature for more about delegate type)
			Func<int, int> square = x => x * x;
			Console.WriteLine(square(5));
			//Expression lambdas can also be converted to the expression tree types, as the following example shows:
			System.Linq.Expressions.Expression<Func<int, int>> e = x => x * x;
			Console.WriteLine(e);
			//
			int[] numbers = { 2, 3, 4, 5 };
			var squaredNumbers = numbers.Select(x => x * x);
			Console.WriteLine(string.Join(" ", squaredNumbers));


			//2. Statement lambda   eg. (input-parameters) => { <sequence-of-statements> }
			Action<string> greet = name =>
			{
				string greeting = $"Hello {name}!";
				Console.WriteLine(greeting);
			};
			greet("World");

			Action line = () => Console.WriteLine();
			line();

			Func<double, double> cube = x => x * x * x;
			cube(5);

			Func<int, int, bool> testForEquality = (x, y) => x == y;
			testForEquality(3, 3);

			//input , input , output   functionname = parameter,parameter , method body or expression.
			Func<int, int, int> constant = (_, _) => 42;
			constant(1,1);

			//Lambda expressions and tuples
			Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
			var numbers3 = (2, 3, 4);
			var doubledNumbers = doubleThem(numbers3);
			Console.WriteLine($"The set {numbers} doubled: {doubledNumbers}");
			// Output:
			// The set (2, 3, 4) doubled: (4, 6, 8)

			//Lambdas with the standard query operators
			#region Func<TResult> Delegate
			//https://learn.microsoft.com/en-us/dotnet/api/system.func-1?view=net-8.0
			//Func<TResult> Delegate
			LambdaDeligateField<int> lazyOne = new LambdaDeligateField<int>(() => ExpensiveOne());
			LambdaDeligateField<long> lazyTwo = new LambdaDeligateField<long>(() => ExpensiveTwo("apple"));

			Console.WriteLine(lazyOne.Value);
			Console.WriteLine(lazyTwo.Value);

			static int ExpensiveOne()
			{
				Console.WriteLine("\nExpensiveOne() is executing.");
				return 1;
			}

			static long ExpensiveTwo(string input)
			{
				Console.WriteLine("\nExpensiveTwo() is executing.");
				return (long)input.Length;
			}
			#endregion

			object parse = (string s) => int.Parse(s);   // Func<string, int>
			Delegate parse2 = (string s) => int.Parse(s); // Func<string, int>


			//Attributes
			//Func<string?, int?> parse3 = [ProvidesNullCheck] (s) => (s is not null) ? int.Parse(s) : null;
			//var concat = ([DisallowNull] string a, [DisallowNull] string b) => a + b;
			//var inc = [return: NotNullifNotNull(nameof(s))] (int? s) => s.HasValue ? s++ : null;


			//Expression Trees
			// --> https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/expression-trees/expression-trees-building
			//Expression<Func<int>> sum = () => 1 + 2;
			//var one = Expression.Constant(1, typeof(int));
			//var two = Expression.Constant(2, typeof(int));


		}
	}


	public class LambdaDeligateField<T> where T : struct {

		private Nullable<T> value;
		private Func<T> getValue;
        public LambdaDeligateField(Func<T> func)
        {
			value = null;
			getValue = func;
        }

		public T Value
		{
			get
			{
				if(value == null)
					value = getValue();
				return (T)value;
			}
		}
    }

}
