using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice
{
	internal class C2Feature
	{
        public C2Feature()
        {
			//Covariance and Contravariance
			///In C#, covariance and contravariance enable implicit reference conversion for array types,
			///delegate types, and generic type arguments. Covariance preserves assignment compatibility 
			///and contravariance reverses it.

			//The following code demonstrates the difference between assignment compatibility,
			//covariance, and contravariance.

			// Assignment compatibility.
			string str = "test";
			// An object of a more derived type is assigned to an object of a less derived type.
			object obj = str;

			// Covariance.
			IEnumerable<string> strings = new List<string>();
			// An object that is instantiated with a more derived type argument
			// is assigned to an object instantiated with a less derived type argument.
			// Assignment compatibility is preserved.
			IEnumerable<object> objects = strings;

			// Contravariance.
			// Assume that the following method is in the class:
			static void SetObject(object o) { }
			Action<object> actObject = SetObject;
			// An object that is instantiated with a less derived type argument
			// is assigned to an object instantiated with a more derived type argument.
			// Assignment compatibility is reversed.
			Action<string> actString = actObject;





			//The following code example shows covariance and contravariance support for method groups.
			Test();

		}

		#region The following code example shows covariance and contravariance support for method groups.
		static object GetObject()
		{
			return null;
		}
		static void SetObject(object obj)
		{
			Console.WriteLine(obj);
		}

		static string GetString()
		{
			return "A";
		}
		static void SetString(string str)
		{
			Console.WriteLine($"{str}");
		}

		static void Test()
		{
			// Covariance. A delegate specifies a return type as object,  
			// but you can assign a method that returns a string.  
			Func<object> del = GetString;

			// Contravariance. A delegate specifies a parameter type as string,  
			// but you can assign a method that takes an object.  
			Action<string> del2 = SetObject;

			del2.Invoke(del.Invoke().ToString());
		}

		#endregion


	}



}
