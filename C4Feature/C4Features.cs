using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotnetPractice.C4Feature
{
	internal class C4Features
	{
		public void Run()
		{
			//https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-version-history#c-version-40

			//Built-in reference types (C# reference)
			DynamicBinding();

			NamedAndOptionalArguments();
		}

		private void NamedAndOptionalArguments()
		{
			//PrintOrderDetails("Gift Shop", 31, "Red Mug");

			//If you don't remember the order of the parameters but know their names, you can send the arguments in any order.
			//PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
			//PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);

			//Optional arguments
			//		public void ExampleMethod(int required, string optionalstr = "default string",
			//int optionalint = 10)

			//COM interfaces

		}

		private  void DynamicBinding()
		{
			//Built-in reference types (C# reference)

			//The string type
			string a = "hello";
			string b = "h";
			// Append to contents of 'b'
			b += "ello";
			Console.WriteLine(a == b);
			Console.WriteLine(object.ReferenceEquals(a, b));

			//String literals available in C#11
			var literal = """
This is a multi-line
    string literal with the second line indented.
""";
			Console.WriteLine(literal);

			var literal2 = """""
This raw string literal has four """", count them: """" four!
embedded quote characters in a sequence. That's why it starts and ends
with five double quotes.

You could extend this example with as many embedded quotes as needed for your text.
""""";
			Console.WriteLine(literal2);

			var json = """
    {
        "prop": 0
    }
    """;
			var shortText = """He said "hello!" this morning.""";
			string a2 = "\\\u0066\n F";
			Console.WriteLine(a);
			// Output:
			// \f
			//  F
			//The escape code \udddd (where dddd is a four - digit number) represents the Unicode character U + dddd.
			//Eight - digit Unicode escape codes are also recognized: \Udddddddd.

			// @"""Ahoy!"" cried the captain."; // "Ahoy!" cried the captain.

			ReadOnlySpan<byte> AuthWithTrailingSpace = new byte[] { 0x41, 0x55, 0x54, 0x48, 0x20 };
			ReadOnlySpan<byte> AuthStringLiteral = "AUTH "u8;
			byte[] AuthStringLiteral2 = "AUTH "u8.ToArray();


			//*************************************************************************************
			//The delegate type

			

		Action<string> stringAction = str => { };
		Action<object> objectAction = obj => { };

		// Valid due to implicit reference conversion of
		// objectAction to Action<string>, but may fail
		// at run time.
		Action<string> combination = stringAction + objectAction;


		// Creates a new delegate instance with a runtime type of Action<string>.
		Action<string> wrappedObjectAction = new Action<string>(objectAction);

		// The two Action<string> delegate instances can now be combined.
		Action<string> combination2 = stringAction + wrappedObjectAction;


	}

		public delegate void MessageDelegate(string message);
		public delegate int AnotherDelegate(C4Features m, long num);
	}
}
