using System;
using System.Reflection;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	public struct Parameter {
		public Type Type;
		public string Name;

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
	public class ObjectBuilder
	{
		/// <summary>
		/// The object that is being constucted
		/// </summary>
		private Object Constructing;

		/// <summary>
		/// The list of constructors this object has
		/// </summary>
		/// <value>The attributes</value>
		private List<ConstructorInfo> Constructors { get; set; }


		private List<IEnumerable<ParameterInfo>> ConstructorFields { get; set; }
		public List<BindingsTable<Parameter, dynamic>> FilledInformation { get; set; }
		public List<List<Parameter>> RequiredTypes { get; set; }

		public ObjectBuilder(Type t)
		{
			Constructors = new List<ConstructorInfo> ();
			ConstructorFields = new List<IEnumerable<ParameterInfo>> ();
			FilledInformation = new List<BindingsTable<Parameter, dynamic>> ();
			RequiredTypes = new List<List<Parameter>> ();

			Constructors.AddRange(t.GetConstructors ());

			// Add a list of parameters from the constructors
			foreach (ConstructorInfo cinfo in Constructors)
				ConstructorFields.Add (cinfo.GetParameters ());

			// Go through each list of parameters, each representing a constructor
			foreach (IEnumerable<ParameterInfo> constructorInfo in ConstructorFields) {
				FilledInformation.Add (new BindingsTable<Parameter, dynamic> ());
				RequiredTypes.Add (new List<Parameter> ());

				// Add a key-value pair between the type / name of a constructor item, and a placeholder value (null)
				foreach (ParameterInfo i in constructorInfo)
					RequiredTypes.Last().Add (new Parameter (i.ParameterType, i.Name));
					
			}
		}

		private ObjectBuilder(List<BindingsTable<Parameter, Object>> filled, List<ConstructorInfo> constructors, List<List<Parameter>> types)
		{
			FilledInformation = filled;
			Constructors = constructors;
			RequiredTypes = types;
		}

		public ObjectBuilder this[int constructorIndex, string parameterField, dynamic value] 
		{
			get {
				return SetParameter (constructorIndex, new Tuple<string, dynamic> (parameterField, value));
			}
		}

		public ObjectBuilder SetParameter(int constructorIndex, Tuple<string, dynamic> parameter)
		{
			var relevantParameter = RequiredTypes.Get (constructorIndex).First (x => x.Name.Equals (parameter.Item1));

			FilledInformation.Get (constructorIndex).Associate (relevantParameter, parameter.Item2);

			return new ObjectBuilder (FilledInformation, Constructors, RequiredTypes);
		}

		public ObjectBuilder SetParameters(int constructorIndex, IEnumerable<Tuple<string, dynamic>> parameters)
		{
			ObjectBuilder o = this;
			foreach (Tuple<string, dynamic> parameter in parameters) 
				o = o.SetParameter (constructorIndex, parameter);

			return o;
		}

		public Object Instantiate(int constructorIndex)
		{
			var relevantConstructorInfo = FilledInformation.Get (constructorIndex).Values().AsArray();

			try {
				var relevantConstructor = Constructors.Get(constructorIndex);

				//var rObject = relevantConstructor.Invoke(relevantConstructorInfo);
				var rObject = relevantConstructor.Invoke(relevantConstructorInfo);
				return rObject;
			} catch(Exception e) {
				Console.Write (e.StackTrace);
			}

			return null;
		}

		private ObjectBuilder(Object constructing)
		{
			Constructing = constructing;
		}
	}
}

