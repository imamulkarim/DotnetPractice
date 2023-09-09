using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice
{
	internal class C8Feature
	{
		public C8Feature() {

			//Default interface methods
			IWriteLine a;

			//Nullable reference types
			/*
			 * A nullable reference type emits a compiler warning or error if a variable that must not be null is assigned to null 
			 */
			//string? nullableString = null;
			//Console.WriteLine(nullableString.Length); // WARNING: may be null! 

			//Pattern matching enhancements
			var point = new Point3D(1, 2, 3); //x=1, y=2, z=3  
			if (point is Point3D(1, var myY, _))  
			{
				Console.WriteLine("point is sane pattern");
				// Code here will be executed only if 
				// the point .X == 1, myY is a new variable  
				// that can be used in this scope.  
			}

			//Asynchronous streams / Asynchronous disposable
			IAsyncEnumerable<int> enumInt =  FetchIOTData();
			//int r = await ReturnFromEnumAsync(enumInt);

			//Using declarations
			//Enhancement of interpolated verbatim strings
			//Null-coalescing assignment
			//Static local functions
			//Indices and ranges
			{
				Index i1 = 3; // number 3 from beginning  
				Index i2 = ^4; // number 4 from end
				int[] aIndices = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
				Console.WriteLine($"{aIndices[i1]}, {aIndices[i2]}"); // "3, 6"

				var slice = aIndices[i1..i2]; // { 3, 4, 5 }
				 //https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#range-operator-
				string line = "one two three";
				int amountToTakeFromEnd = 5;
				Range endIndices = ^amountToTakeFromEnd..^0;  //it get index position from last (length -5) and (length - 0) // 13 - 5 = 7 to 13 -0 = 13
				string end = line[endIndices];
				Console.WriteLine(end);  // output: three

				int[] numbers = new[] { 0, 10, 20, 30, 40, 50 };

				//You can omit any of the operands of the .. operator to obtain an open-ended range:

				//a.. is equivalent to a..^0
				var a1 = numbers[1..];
				//..b is equivalent to 0..
				var b1 = numbers[..];
				//b.. is equivalent to 0..^0
			}
			//Unmanaged constructed types
			///In C# 7.3 and earlier, a constructed type (a type that includes at least one type of argument)
			///can’t be an unmanaged type. Starting with C# 8.0, a constructed value type is unmanaged if 
			///it contains fields of unmanaged types only.
			/*
			 Public struct Foo<T>   
			{   
				public T Var1;   
				public T Var2;   
			}
			*/
			//Readonly-Member
			new XValue().IncreaseX();
			//Disposable ref structs


			//Null Coalescing Assignment
			//if (variable == null)
			//{
			//	variable = expression; // C# 1..7  
			//}
			//variable ??= expression; // C# 8

			//Alternative interpolated verbatim strings
			//var file = $@"c:\temp\{filename}"; // C# 7  
			//var file = @$"c:\temp\{filename}"; // C# 8

			//Using declarations
			/*
			 * 
			 * // C# Old Style  
				using (var repository = new Repository())    
				{    
				} // repository is disposed here!
       
				// vs.C# 8       
				using var repository = new Repository();    
				Console.WriteLine(repository.First());    
				// repository is disposed here!
			 * */



			//Disposable ref structs
			//			ref struct Test
			//		{
			//			public void Dispose() { ... }
			//		}
			//using var local = new Test();
			//	// local is disposed here!


			//Static Local Functions
			AddFiveAndSeven();
	}

		public struct XValue
		{
			private int X { get; set; }
			public readonly int IncreaseX()
			{
				// This will not compile: C# 8 
				// X = X + 1; 
				var newX = X + 1; // OK 
				return newX;
			}
		}

		int AddFiveAndSeven()
		{
			int y = 5;
			int x = 7;
			return Add(x, y);
			static int Add(int left, int right) => left + right;
			//https://stackoverflow.com/questions/58745614/why-declare-a-local-function-static-in-c-sharp-8-0
			//Because it prevents you from shooting yourself in the foot. It forces the local function to be a pure function that does not modify the state of the caller.
			//Because it prevents you from shooting yourself in the foot. It forces the local function to be a pure function that does not modify the state of the caller.

		}


		public async Task<int> ReturnFromEnumAsync(IAsyncEnumerable<int> enumInt)
		{
			await foreach (var x in enumInt)
			{
				if (x == 3)
					return x;
			}
			return 0;

		}

		static async IAsyncEnumerable<int> FetchIOTData()
		{
			for (int i = 1; i <= 10; i++)
			{
				await Task.Delay(100);//Simulate waiting for data to come through. 
				yield return i;
			}
		}

	}

	record Point3D(int a, int b, int c);

	

	interface IWriteLine
	{
		public void WriteLine()
		{
			Console.WriteLine("Wow C# 8!");
		}
	}
}
