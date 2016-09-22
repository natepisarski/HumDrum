using System;
using HumDrum.Collections;

using System.Collections.Generic;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// The Database class controls a list of Tables, which each manage a list of
	/// Columns.
	/// </summary>
	public class Database
	{
		/// <summary>
		/// The tables that are managed by this Database.
		/// </summary>
		/// <value>The tables, managed in a list.</value>
		public List<Table> Tables { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.Database"/> class.
		/// This will create a database with an empty list of tables.
		/// </summary>
		public Database ()
		{
			Tables = new List<Table> ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.Database"/> class.
		/// This will use an initial listing
		/// </summary>
		/// <param name="tables">The list of tables that this Database should manage</param>
		public Database(IEnumerable<Table> tables)
		{
			Tables = new List<Table> ();
			Tables.AddRange (tables);
		}

		/// <summary>
		/// Returns the table that shares the name with this
		/// </summary>
		/// <returns>The table that has this name</returns>
		/// <param name="name">The name of the table</param>
		public Table GetTable(string name)
		{
			return Tables.DoTo (x => x.Title.Equals (name), (x => x));
		}


	}
}

