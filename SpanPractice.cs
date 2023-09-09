using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
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

			//Marshalling is a "medium" for want of a better word or a gateway, to communicate with the 
			//unmanaged world's data types and vice versa


			Console.WriteLine("Pointer by Marshal");
			IntPtr ptr = Marshal.AllocHGlobal(1);
			try
			{
				Span<byte> bytesPointer;
				unsafe { bytesPointer = new Span<byte>((byte*)ptr, 1); }
				bytesPointer[0] = 42;

				Console.WriteLine(Marshal.ReadByte(ptr));
				Console.WriteLine(bytesPointer[0]);
				//Assert.Equal(42, bytesPointer[0]);
				//Assert.Equal(Marshal.ReadByte(ptr), bytesPointer[0]);
				//bytesPointer[1] = 43; // Throws IndexOutOfRangeException
			}
			finally { Marshal.FreeHGlobal(ptr); }


			Console.WriteLine("Span Struct ");
			Span<MutableStruct> spanOfStructs = new MutableStruct[1];
			spanOfStructs[0].Value = 42;
			Console.WriteLine(spanOfStructs[0].Value);
			Console.WriteLine(42);
			//Assert.Equal(42, spanOfStructs[0].Value);
			var listOfStructs = new List<MutableStruct> { new MutableStruct() };
			//Console.WriteLine(listOfStructs[0].Value); //.Value = 42; // Error CS1612: the return value is not a variable

			var values = new int[] { 42, 84, 126 };
			AddOne(ref values[2]);
			Console.WriteLine( values[2]);

		}

		//static Span<char> FormatGuid( Guid guid)
		//{
		//	Span<char> chars = stackalloc char[100];
		//	bool formatted = guid.TryFormat(chars, out int charsWritten, "d");
		//	Debug.Assert(formatted);
		//	return chars.Slice(0, charsWritten); // Uh oh
		//}

		public static void AddOne(ref int value) => value += 1;


		struct MutableStruct { public int Value; }

	}
}
