using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.C3Feature
{
	internal class CovarianceContravariance
	{
		//https://code-maze.com/csharp-covariance-and-contravariance/
		//Covariance in C# is a concept of preserving assignment compatibility.
		//It allows us to assign an object, variable, or parameter of a more derived type
		//to an object, variable, or parameter of a less derived type.
		delegate void personDelegate(Employee employee);

		public void Covariance() {

			var personObject = new Person();
			var employeeObject = new Employee();
			var managerObject = new Manager();

			personObject = employeeObject;
			personObject = managerObject;


			//array
			Person[] people = new Employee[5];
			people[0] = new Manager();  // 

			//generic

			ICovariant<Person> icovPerson = new ImplementICovariant<Person>();
			ICovariant<Employee> icovEmployee = new ImplementICovariant<Employee>();

			icovPerson = icovEmployee;

		}

		//Contravariance in C# is the opposite of covariance. It allows us to reverse assignment compatibility preserved by covariance
		public void Contravariance() {
			//employeeObject = personObject;  // Compile error: Cannot implicitly convert type 'Person' to 'Employee'.
			//managerObject = personObject;   // Compile error: Cannot implicitly convert type 'Person' to 'Manager'.

			static void GreetPerson(Person person)
			{
				// Logic to greet person.
				Console.WriteLine(person.GetValue());
			}

			personDelegate del = GreetPerson;

			var personObject = new Person();
			var employeeObject = new Employee();
			var managerObject = new Manager();

			//Person[] people = { new Person() };
			//Employee[] employees = people;// Compile error: Cannot implicitly convert type 'Person[]' to 'Employee[]'.

			del(employeeObject);
			//(del.Method.ToString() == "Void GreetPerson(Tests.Person)");

			//another approach
			Action<Person> b = (target) => { Console.WriteLine(target.GetType().Name); };
			Action<Employee> d = b;
			d(new Employee());

			//generic

			IContravariant<Person> icontraPerson = new ImplementIContravariant<Person>();
			IContravariant<Employee> icontraEmployee = new ImplementIContravariant<Employee>();
			icontraEmployee = icontraPerson;

		}


	}

	//generic
	interface ICovariant<out T> { }
	class ImplementICovariant<T> : ICovariant<T> { }

	interface IContravariant<in T> { }
	class ImplementIContravariant<T> : IContravariant<T> { }


	public class Person { public virtual string GetValue() => "Person"; }
	public class Employee : Person { public override string GetValue() => "Employee"; }
	public class Manager : Employee { public override string GetValue() => "Manager"; }

	

}
