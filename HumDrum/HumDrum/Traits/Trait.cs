using System;
using System.Reflection;

using HumDrum.Structures;

namespace HumDrum.Traits
{
	/// <summary>
	/// A Trait is a description of a class's capabilities.
	/// This closely resembles an interface, but with one key difference - 
	/// interfaces must be implemented by a class within it, never after.
	/// 
	/// Traits allow you to satiate the requirements of an interface separately
	/// from the class definition, which lets you describe how some class that 
	/// you did not make interacts with an interface. 
	/// </summary>
	public class Trait
	{
		/// <summary>
		/// The name of this trait
		/// </summary>
		/// <value>The name of the trait</value>
		public string Name {get; set;}

		/// <summary>
		/// The Interface which describes 
		/// </summary>
		/// <value>The required methods.</value>
		public Interface RequiredMethods { get; set; }

		/// <summary>
		/// The class which implements the methods that the interface requires
		/// </summary>
		/// <value>The implementing class</value>
		public Class ImplementingClass { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Trait"/> class.
		/// This will not test to see if 
		/// </summary>
		/// <param name="blueprint">Blueprint.</param>
		/// <param name="implementor">Implementor.</param>
		public Trait (Interface blueprint, Class implementor)
		{
			RequiredMethods = blueprint;
			ImplementingClass = implementor;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Trait"/> class.
		/// This uses two traits which are immediately cast to an Interface and Class.
		/// </summary>
		/// <param name="theInterface">The interface</param>
		/// <param name="theImplementor">The implementor</param>
		public Trait (Type theInterface, Type theImplementor)
		{
			RequiredMethods = new Interface (theInterface);
			ImplementingClass = new Class (theImplementor);
		}

		/// <summary>
		/// Adds a Method to the implementor class
		/// </summary>
		/// <param name="m">The method</param>
		public void AddMethod(Method m)
		{
			ImplementingClass.AddMethod (m);
		}

		/// <summary>
		/// Adds a Type t to the InnerClass as a Method
		/// </summary>
		/// <param name="t">The method to add</param>
		public void AddMethod(Type typ, string methodName)
		{
			AddMethod (new Method(typ.GetMethod(methodName)));
		}

		/// <summary>
		/// Adds a Method from MethodInfo, binding it optionally with a name
		/// </summary>
		/// <param name="mi">The method info</param>
		/// <param name="methodName">The name of the method</param>
		public void AddMethod(MethodInfo mi, string methodName)
		{
			AddMethod (new Method (mi, methodName));
		}

		/// <summary>
		/// Coerces this Delegate into a Method and adds it to this trait
		/// </summary>
		/// <param name="d">The delegate to add to this trait</param>
		/// <param name="methodName">The name of the method to add</param>
		public void AddMethod(string methodName, Delegate d)
		{
			AddMethod (d.Method, methodName);
		}

		/// <summary>
		/// Tests to see whether or not the implementor satisfies the given class
		/// </summary>
		/// <returns><c>true</c>, if satisfy was doesed, <c>false</c> otherwise.</returns>
		public bool IsSatisfied()
		{
			foreach (Method m in RequiredMethods.Methods()) {
				if (!ImplementingClass.HasMethod (m))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Gets a named Method from the implementing class
		/// </summary>
		/// <param name="functionName">Function name</param>
		public Method The(string functionName)
		{
			foreach (Method m in ImplementingClass.Methods()) {
				if (m.Name.Equals (functionName))
					return m;
			}
			return null;
		}
	}
}