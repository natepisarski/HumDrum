using System;
using System.Collections.Generic;

using System.Reflection;
namespace HumDrum.Traits
{
	/// <summary>
	/// Represents an interface
	/// </summary>
	public class Interface
	{
		/// <summary>
		/// The Interface, held here as a Type
		/// </summary>
		/// <value>The basic type</value>
		public Type BasicType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Interface"/> class.
		/// This will ensure that this type is an interface
		/// </summary>
		/// <param name="type">The type to attempt to initialize</param>
		public Interface (Type type)
		{
			if (!type.IsInterface)
				throw new Exceptions.TypeNotInterfaceException (type.ToString () + " was not an interface");
			BasicType = type;
		}

		/// <summary>
		/// Returns the Methods that this Interface has.
		/// </summary>
		public IEnumerable<Method> Methods()
		{
			foreach (MethodInfo mi in BasicType.GetMethods())
				yield return new Method (mi);
			yield break;
		}
	}
}

