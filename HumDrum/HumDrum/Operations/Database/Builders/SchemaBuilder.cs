using System;

using HumDrum.Collections;
namespace HumDrum.Operations.Database
{
	/// <summary>
	/// SchemaBuilder is a fluent interface to the Schema object.
	/// </summary>
	public class SchemaBuilder 
	{
		/// <summary>
		/// The schema that this builder is working with
		/// </summary>
		/// <value>The working schema</value>
		private Schema WorkingSchema { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.SchemaBuilder"/> class.
		/// This uses the given schema to initalize the working schema
		/// </summary>
		/// <param name="workingSchema">The schema to use internally</param>
		private SchemaBuilder(Schema workingSchema)
		{
			WorkingSchema = workingSchema;
		}

		/// <summary>
		/// Returns a reference to this object
		/// </summary>
		private SchemaBuilder Reference()
		{
			return new SchemaBuilder (WorkingSchema);
		}

		/// <summary>
		/// Starts the SchemaBuilder with a blank Table schema
		/// </summary>
		public static SchemaBuilder Start()
		{
			return new SchemaBuilder (new Schema ());
		}

		/// <summary>
		/// Adds a SchemaAtom to the current working schema
		/// </summary>
		/// <param name="atom">The atom to add to the schema</param>
		public SchemaBuilder Add(SchemaAtom atom)
		{
			WorkingSchema.Add (atom);
			return Reference ();
		}

		/// <summary>
		/// Adds a new Atom to this schema using its components
		/// </summary>
		/// <param name="name">The name for this column</param>
		/// <param name="type">The type for this column</param>
		public SchemaBuilder Add(string name, Type type)
		{
			return Add (new SchemaAtom (name, type));
		}

		/// <summary>
		/// Returns the actual schema
		/// </summary>
		public Schema Finalize()
		{
			return WorkingSchema;
		}
	}
}

