using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice
{
	internal class SpanPractice
	{
		public void Do()
		{
			//var arr = new byte[10];
			//Span<byte> bytes = arr; // Implicit cast from T[] to Span<T>

			//var arr2 = new People[10];
			//Span<People> bytes2 = arr2; // Implicit cast from T[] to Span<T>

			Span<int> bytes = stackalloc int[2]; // Using C# 7.2 stackalloc support for spans
			bytes[0] = 42;
			bytes[1] = 43;

			//Assert.Equal(42, bytes[0]);
			//Assert.Equal(43, bytes[1]);

			#region stackalloc
			//A stackalloc expression allocates a block of memory on the stack
			//Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
			//var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
			//Console.WriteLine(ind);  // output: 1

			//unsafe
			//{
			//	int length = 3;
			//	int* numbersP = stackalloc int[length];
			//	for (var i = 0; i < length; i++)
			//	{
			//		numbersP[i] = i;

			//		Console.WriteLine(numbersP[i]);
			//	}
			//}

			//Span<int> first = stackalloc int[3] { 1, 2, 3 };
			//Span<int> second = stackalloc int[] { 1, 2, 3 };
			//ReadOnlySpan<int> third = stackalloc[] { 1, 2, 3 };

			#endregion

			//Marshalling is a "medium" for want of a better word or a gateway, to communicate with the unmanaged world's data types and vice versa
		}
	}
}
