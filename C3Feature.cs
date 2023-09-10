using System;
using System.Collections.Generic;
using System.Linq;
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

		}
	}
}
