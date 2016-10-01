using System;
using System.Collections.Generic;

using HumDrum.Collections;
namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Contains a list of schemas present in a table. For a schema
	/// for an individal column, see: <see cref="HumDrum.Operations.DatabBase.SchemaAtom"/> 
	/// </summary>
	public class Schema
	{
		/// <summary>
		/// The complete schema for a table
		/// </summary>
		/// <value>The schema</value>
		public List<SchemaAtom> TableSchema {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.Schema"/> class.
		/// This will get the schema from the table
		/// </summary>
		/// <param name="table">The table</param>
		public Schema (Table table)
		{
			TableSchema = new List<SchemaAtom> ();

			foreach (Column c in table.Columns) {
				TableSchema.Add (new SchemaAtom (c));
			}
		}

		/// <summary>
		/// Initializes a blank Schema
		/// </summary>
		public Schema()
		{
			TableSchema = new List<SchemaAtom> ();
		}

		/// <summary>
		/// Tests to see if this schema has an item with the given title and type
		/// </summary>
		/// <returns><c>true</c> if this instance has type title; otherwise, <c>false</c>.</returns>
		/// <param name="type">The type to check</param>
		/// <param name="title">The title of the column</param>
		public bool Has(Type type, string title) {
			return 
			TableSchema.Any (x => x.ColumnType.Equals (type)) &&
			TableSchema.Any (x => x.ColumnName.Equals (title));
		}

		/// <summary>
		/// Tests to see if the schemas are compatible. This will compare both names and types for the columns.
		/// </summary>
		/// <param name="otherSchema">The other schema to compare this one with</param>
		public bool Matches(Schema otherSchema) {
			foreach (SchemaAtom atom in TableSchema) {
				if (!(otherSchema.Has (atom.ColumnType, atom.ColumnName)))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Adds a new Column Type to this schema
		/// </summary>
		/// <param name="atom">The name and type of the column to add</param>
		public void Add(SchemaAtom atom) {
			TableSchema.Add (atom);
		}

		/// <summary>
		/// Adds a collection of SchemaAtoms to this table in the order they appear in
		/// </summary>
		/// <param name="atoms">The atoms to push onto the table</param>
		public void Add(IEnumerable<SchemaAtom> atoms) {
			foreach (SchemaAtom a in atoms)
				Add (a);
		}

		/// <summary>
		/// This will append the entirety of the schema in the argument to the current schema
		/// </summary>
		/// <param name="schema">The schema to append</param>
		public void Add(Schema schema) {
			foreach (SchemaAtom atom in schema.TableSchema)
				this.TableSchema.Add (atom);
		}

		/// <summary>
		/// Determines whether the specified <see cref="HumDrum.Operations.Database.Schema"/> is equal to the current <see cref="HumDrum.Operations.Database.Schema"/>.
		/// </summary>
		/// <param name="otherSchema">The <see cref="HumDrum.Operations.Database.Schema"/> to compare with the current <see cref="HumDrum.Operations.Database.Schema"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="HumDrum.Operations.Database.Schema"/> is equal to the current
		/// <see cref="HumDrum.Operations.Database.Schema"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (Object otherSchemaObject)
		{
			Schema otherSchema = (Schema)otherSchemaObject;

			foreach (SchemaAtom atom in TableSchema)
				if (!(otherSchema.Has (atom.ColumnType, atom.ColumnName)))
					return false;
			return true;
		}
	}
}

