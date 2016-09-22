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
		public static Table CartesianProduct(Table table1, Table table2)
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
	}
}

