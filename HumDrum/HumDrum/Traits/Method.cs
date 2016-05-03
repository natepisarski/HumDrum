using System;
using System.Collections.Generic;
using System.Reflection;

using HumDrum.Collections;

namespace HumDrum.Traits
{
	//TODO: Make this easier to invoke on things

	/// <summary>
	/// Represents a C# Method, based only on
	/// return values and parameter types
	/// </summary>
	public class Method
	{
		/// <summary>
		/// The identifier of this method
		/// </summary>
		/// <value>The name</value>
		public string Name {get; set;}

		/// <summary>
		/// The MethodInfo associated with this method
		/// </summary>
		/// <value>The basic method</value>
		public MethodInfo BasicMethod {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Method"/> class.
		/// </summary>
		/// <param name="method">The method to use for naming and as the basic method</param>
		public Method(MethodInfo method)
		{
			if (method == null)
				throw new Exceptions.MethodNotImplementedException ();
			
			Name = method.Name;
			BasicMethod = method;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Traits.Method"/> class.
		/// This will rename the method given a Name as a String. The BasicMethod will still
		/// contain the name of its own member, however.
		/// </summary>
		/// <param name="method">The method to add</param>
		/// <param name="name">The custom name of this method</param>
		public Method (MethodInfo method, string name)
		{
			Name = name;
			BasicMethod = method;
		}


		/// <summary>
		/// Gets the return type of this method
		/// </summary>
		public Type GetReturnType()
		{
			return BasicMethod.ReturnType;
		}

		/// <summary>
		/// Returns, sequentially, the list of the type that the parameters take
		/// </summary>
		/// <returns>The parameter types.</returns>
		public IEnumerable<Type> GetParameterTypes()
		{
			foreach (ParameterInfo pInfo in BasicMethod.GetParameters()) 
				yield return pInfo.ParameterType;
			
			yield break;
		}


		/// <summary>
		/// Determines whether the specified <see cref="HumDrum.Traits.Method"/> is equal to the current <see cref="HumDrum.Traits.Method"/>.
		/// </summary>
		/// <param name="otherMethod">The <see cref="HumDrum.Traits.Method"/> to compare with the current <see cref="HumDrum.Traits.Method"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="HumDrum.Traits.Method"/> is equal to the current
		/// <see cref="HumDrum.Traits.Method"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals(Object secondMethod)
		{
			Method otherMethod;
			if (secondMethod == null)
				return false;
			else
				otherMethod = secondMethod as Method;
			return (GetReturnType ().Equals (otherMethod.GetReturnType ())
				&& Information.Equal (GetParameterTypes (), otherMethod.GetParameterTypes ())
				&& Name.Equals(otherMethod.Name));
		}

	}
}

