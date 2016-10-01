using System;
using HumDrum.Collections;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Operations that can be performed between Database tables.
	/// </summary>
	public static class DatabaseOperations
	{
		/// <summary>
		/// Returns an anonymous table that is the cartesian product of two tables.
		/// </summary>
		/// <returns>The new, nameless, table that is the product between the two tables.</returns>
		/// <param name="table1">The lval table.</param>
		/// <param name="table2">The rval table.</param>
		public static Table CartesianProduct(this Table table1, Table table2)
		{
			if ((table1.GetSchema ().Matches (table2.GetSchema ())))
				return null; // You can only find the cartesian product given that the two DO NOT MATCH
			
			Table t = new Table ("");

			t.AppendSchema (table1.GetSchema ());
			t.AppendSchema (table2.GetSchema ());

			foreach (Row row in table1.Rows) {
				foreach (Row otherRow in table2.Rows) {
					t.AppendRow (row);
					t.Rows.Last ().Add (otherRow);
				}
			}

			return t;
		}

		/// <summary>
		/// ImplicitJoin takes two tables, and creates a third table with no merging of rows.
		/// It qualifies every column with the name of the table. So, a table called "Animals" under
		/// an implicit join may contain rows of the form "Animals.Species, Animals.Name", etc.
		/// 
		/// Note that this can conveniently be cascaded upton to join more than two tables.
		/// </summary>
		/// <returns>The conjunction of the two tables.</returns>
		/// <param name="table1">The first table to join</param>
		/// <param name="table2">The second table to join</param>
		public static Table ImplicitJoin(this Table table1, Table table2) {
			Table t = new Table ("");

			foreach (Column table1Column in table1.Columns)
				t.AddColumn (new Column (table1.Title + "." + table1Column.Title, table1Column.ColumnType, table1Column.Data));

			foreach (Column table2Column in table2.Columns)
				t.AddColumn (new Column (table2.Title + "." + table2Column.Title, table2Column.ColumnType, table2Column.Data));

			return t;
		}
	}
}

