using System;
using System.Reflection;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	[Stable]
	/// <summary>
	/// Represents a Parameter from a constructor field.
	/// This simply binds together a name and a type.
	/// </summary>
	public struct Parameter {

		/// <summary>
		/// The type of this parameter
		/// </summary>
		public Type Type;

		/// <summary>
		/// The name of the parameter
		/// </summary>
		public string Name;

		/// <summary>
		/// Creates a new Parameter, with the given name and type.
		/// </summary>
		/// <param name="type">The type of this parameter</param>
		/// <param name="name">The name of this parameter</param>
		public Parameter(Type type, string name) {
			Type = type;
			Name = name;
		}
	}

	/// <summary>
	/// ObjectBuilder is a generic factory which enables cascading-style building
	/// of any object's public attributes using Reflection. This class only accesses the 
	/// fields specified in an Object's constructor.
	/// </summary>
	[Experimental]
	public sealed class ObjectBuilder
	{
		/// <summary>
		/// The list of constructors this object has
		/// </summary>
		/// <value>The ConstructorInfo for each constructor, in order of appearance in the file itself</value>
		private List<ConstructorInfo> Constructors { get; set; }

		/// <summary>
		/// Contains a list of parameters and any values which have been supplied to them,
		/// either through the indexor or SetParameter. This will be used to instantiate the object.
		/// 
		/// Each individual BindingsTable represents a different constructor. 
		/// </summary>
		/// <value>The filled-in information as a list of bindings tables.</value>
		public List<BindingsTable<Parameter, dynamic>> FilledInformation { get; set; }

		/// <summary>
		/// Contains a list of parameter lists which contain the type information for every constructor.
		/// </summary>
		/// <value>The required types</value>
		public List<List<Parameter>> RequiredTypes { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Structures.ObjectBuilder"/> class.
		/// This creates an ObjectBuilder that works on the given type. When this is called, it automatically
		/// fills in a list of Constructors and the required types for each constructor's parameters.
		/// </summary>
		/// <param name="t">The type of object you want to build</param>
		public ObjectBuilder(Type t)
		{
			Constructors = new List<ConstructorInfo> ();
			FilledInformation = new List<BindingsTable<Parameter, dynamic>> ();
			RequiredTypes = new List<List<Parameter>> ();

			// The actual parameters, garnered from Reflection
			var ConstructorFields = new List<IEnumerable<ParameterInfo>> ();

			// Add all the constructors
			Constructors.AddRange(t.GetConstructors ());

			// Add a list of parameters from the constructors
			foreach (ConstructorInfo cinfo in Constructors)
				ConstructorFields.Add (cinfo.GetParameters ());

			// Go through each list of parameters, each representing a constructor
			foreach (IEnumerable<ParameterInfo> constructorInfo in ConstructorFields) {

				// Make a new BindingsTable for every constructor
				FilledInformation.Add (new BindingsTable<Parameter, dynamic> ());

				// Also, fill in the types each constructor needs
				RequiredTypes.Add (new List<Parameter> ());

				// Add a key-value pair between the type / name of a constructor item, and a placeholder value (null)
				foreach (ParameterInfo i in constructorInfo)
					RequiredTypes.Last().Add (new Parameter (i.ParameterType, i.Name));
					
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Structures.ObjectBuilder"/> class.
		/// This private constructor is used to create a new object from existing Fields for the 
		/// purpose of cascading.
		/// </summary>
		/// <param name="filled">The information that has already been filled in</param>
		/// <param name="constructors">Each constructor this ObjectBuilder has</param>
		/// <param name="types">The types that must be specified</param>
		private ObjectBuilder(List<BindingsTable<Parameter, Object>> filled, List<ConstructorInfo> constructors, List<List<Parameter>> types)
		{
			FilledInformation = filled;
			Constructors = constructors;
			RequiredTypes = types;
		}

		/// <summary>
		/// The indexer for this ObjectBuilder. The indexer actually returns a new ObjectBuilder
		/// object wrapping the constructor at the index, with the parameterField filled in with a new value.
		/// </summary>
		/// <param name="constructorIndex">Which constructor to use, with 0 being the first constructor</param>
		/// <param name="parameterField">The name of the parameter to fill in</param>
		/// <param name="value">The value of the parameter with the identifier "parameterField"</param>
		public ObjectBuilder this[int constructorIndex, string parameterField, object value] 
		{
			get {
				return SetParameter (constructorIndex, parameterField, value);
			}
		}

		/// <summary>
		/// Sets the given parameter of the constructor at constructorIndex equal to the specified value,
		/// and returns the new ObjectBuilder with this information implanted in it.
		/// </summary>
		/// <returns>The new ObjectBuilder.</returns>
		/// <param name="constructorIndex">Which constructor you would like to use</param>
		/// <param name="parameter">The value to set the parameter to</param>
		public ObjectBuilder SetParameter(int constructorIndex, Tuple<string, object> parameter)
		{
			// Gets the information about the first parameter to have the name within this tuple
			var relevantParameter = RequiredTypes.Get (constructorIndex).First (x => x.Name.Equals (parameter.Item1));

			// Associate the name to the given value in the BindingsTable
			FilledInformation.Get (constructorIndex).Associate (relevantParameter, parameter.Item2);

			// Return an ObjectBuilder with this information
			return new ObjectBuilder (FilledInformation, Constructors, RequiredTypes);
		}

		/// <summary>
		/// Sets the parameter from the constructor with the given constructorIndex with the given name
		/// to the specified value
		/// </summary>
		/// <returns>The new ObjectBuilder with this information set in</returns>
		/// <param name="constructorIndex">The index of the constructor, starting at 0.</param>
		/// <param name="name">The name of the parameter</param>
		/// <param name="parameterValue">The value to fill in for the parameter</param>
		public ObjectBuilder SetParameter(int constructorIndex, string name, object parameterValue)
		{
			return SetParameter (constructorIndex, new Tuple<string, object> (name, parameterValue));
		}

		/// <summary>
		/// Sets the parameter for a list of tuples, specifying multiple parameters.
		/// </summary>
		/// <returns>The ObjectBuilder with this information implanted in it</returns>
		/// <param name="constructorIndex">The index of the constructor, starting with 0</param>
		/// <param name="parameters">The list of parameters to give this ObjectBuilder</param>
		public ObjectBuilder SetParameter(int constructorIndex, IEnumerable<Tuple<string, object>> parameters)
		{
			ObjectBuilder o = this;

			foreach (Tuple<string, dynamic> parameter in parameters) 
				o = o.SetParameter (constructorIndex, parameter);

			return o;
		}

		/// <summary>
		/// Instantiate the Object, creating an object which can be cast to the target type.
		/// </summary>
		/// <param name="constructorIndex">The index for the constructor you would like to use to instnatiate</param>
		public object Instantiate(int constructorIndex)
		{
			// Get the constructor's values which have been filled in for the proper constructor
			var relevantConstructorInfo = FilledInformation.Get (constructorIndex).Values().AsArray();

			try {
				// Get the right constructor
				var relevantConstructor = Constructors.Get(constructorIndex);

				// Make the object
				var rObject = relevantConstructor.Invoke(relevantConstructorInfo);

				// Return it
				return rObject;
			} catch(Exception e) {

				// It can go wrong if a parameter is of an incorrect tpye
				Console.Write (e.StackTrace);
			}

			return null;
		}
	}
}