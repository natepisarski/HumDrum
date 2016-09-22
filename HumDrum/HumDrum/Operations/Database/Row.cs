using System;
using System.Collections.Generic;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Creates a Row given a table and some instructions. Table should handle 
	/// the actual specification of what exactly is included in this row
	/// </summary>
	public class Row
	{
		public List<RowItem> Items {get; set;}

		/// <summary>
		/// The schema for this particular row
		/// </summary>
		/// <value>The row schema</value>
		public Schema RowSchema { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.Row"/> class.
		/// This will use a table and a given row index to get a row from
		/// </summary>
		/// <param name="table">The table to get the row from</param>
		/// <param name="index">The index of the row, starting with 0</param>
		public Row (Table table, int index)
		{
			Items = new List<RowItem> ();
			RowSchema = table.GetSchema ();

			foreach (Column c in table.Columns) 
				Items.Add (new RowItem (c.ColumnType, c.ItemAt<Object> (index), c));
		}

		/// <summary>
		/// Add the given schema to the Row. Not advised unless you're also adding RowItems.
		/// </summary>
		/// <param name="schema">The schema to append to this table</param>
		public void Add(Schema schema) {

			// First we have to add the schema for the row
			foreach (SchemaAtom atom in schema.TableSchema)
				RowSchema.Add (atom);
		}

		/// <summary>
		/// Adds the entire row to this current row
		/// </summary>
		/// <param name="row">The row to append to this row</param>
		public void Add(Row row) {
			Add (row.RowSchema);

			foreach (RowItem item in row.Items)
				Items.Add (item);
		}
	}
}

