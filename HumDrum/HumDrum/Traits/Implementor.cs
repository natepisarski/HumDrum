using System;
using System.Collections.Generic;

namespace HumDrum.Traits
{
	/// <summary>
	/// Represents an implementor of certain classes
	/// </summary>
	public class Implementor
	{
		/// <summary>
		/// The list of Interfaces that this Implementor implements
		/// </summary>
		/// <value>The implements.</value>
		private List<Interface> Implements { get; set; }

		/// <summary>
		/// The class which implements all of the Implements methods
		/// </summary>
		/// <value>The implementing class</value>
		public Class ImplementingClass { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Implementor"/> class.
		/// </summary>
		/// <param name="interfaces">Interfaces.</param>
		public Implementor (Class theClass, Interface interfaces)
		{
			Implements = new List<Interface> ();

			ImplementingClass = theClass;

			Implements.AddRange (interfaces);
		}

		/// <summary>
		/// Gets the method with the specified name from this implementor
		/// </summary>
		/// <param name="name">The name to search for</param>
		public Method The(string name)
		{
			return ImplementingClass.GetMethod (name);
		}
	}
}

