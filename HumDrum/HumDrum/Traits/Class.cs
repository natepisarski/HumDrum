using System;
using System.Collections.Generic;
using System.Reflection;

using HumDrum.Collections;

namespace HumDrum.Traits
{
	/// <summary>
	/// This class represents a Class Object itself.
	/// It enforces that some reflected type is a class
	/// and provides facilities for interacting with the
	/// class through reflection.
	/// </summary>
	public class Class
	{
		/// <summary>
		/// The basic type of this class as a Type object
		/// </summary>
		/// <value>The type of this object</value>
		public Type BasicType { get; private set;}

		/// <summary>
		/// Gets or sets a list of methods that this class
		/// is picking up for a trait
		/// </summary>
		/// <value>The method bank</value>
		public List<Method> MethodBank { get; set; }

		/// <summary>
		/// Attempts to make a Class with a given type. 
		/// </summary>
		/// <param name="type">The type to make the class with</param>
		public Class (Type type)
		{
			MethodBank = new List<Method> ();

			if (!type.IsClass)
				throw new Exceptions.TypeNotClassException (type.ToString () + " was not a valid class");
			BasicType = type;
		}

		/// <summary>
		/// Returns a list of all the methods that the BasicType of this class has
		/// </summary>
		public IEnumerable<Method> Methods()
		{
			foreach (MethodInfo info in BasicType.GetMethods())
				yield return new Method (info);

			foreach (Method m in MethodBank)
				yield return m;
			
			yield break;
		}

		/// <summary>
		/// Get a method with the specified name
		/// </summary>
		/// <returns>The method to return</returns>
		/// <param name="name">The name to search for</param>
		public Method GetMethod(string name)
		{
			foreach (Method m in Methods())
				if (m.Name.Equals (name))
					return m;
			return null;
		}

		/// <summary>
		/// Check to see if this class or its Method Bank have a method
		/// </summary>
		/// <returns><c>true</c> if this instance has method the specified method; otherwise, <c>false</c>.</returns>
		/// <param name="method">Method.</param>
		public bool HasMethod(Method method)
		{
			return Methods ().Has (method);
		}

		/// <summary>
		/// Adds a method to this class
		/// </summary>
		/// <param name="m">the method to add</param>
		public void AddMethod(Method m)
		{
			MethodBank.Add (m);
		}
	}
}

