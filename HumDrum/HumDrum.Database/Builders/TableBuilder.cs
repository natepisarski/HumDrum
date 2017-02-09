using System;
using HumDrum.Collections;

namespace HumDrum.Operations.Database
{

	/// <summary>
	/// The TableBuilder is the fluent interface for entire tables within a Database.
	/// Using SchemaBuilder and ColumnBuilder inside of these methods allows entire tables
	/// to be created with one call to TableBuilder.
	/// </summary>
	[Experimental]
	public class TableBuilder
	{
		/// <summary>
		/// Returns a blank TableBuilder object
		/// </summary>
		public static TableBuilder Start()
		{
			return new TableBuilder ();
		}

		/// <summary>
		/// The internal table that the program will be manipulating
		/// </summary>
		/// <value>The inner table</value>
		public Table InnerTable { get; set; }

		/// <summary>
		/// Creates a TableBuilder instance where the name is left blank
		/// </summary>
		public TableBuilder ()
		{
			InnerTable = new Table ("");
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Database.TableBuilder"/> class.
		/// This will create an empty table that can be manipulated
		/// </summary>
		/// <param name="title">The title for the table being built</param>
		public TableBuilder (string title)
		{
			InnerTable = new Table (title);
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Database.TableBuilder"/> class.
		/// The constructor used internally to build a table
		/// </summary>
		/// <param name="representation">Representation.</param>
		private TableBuilder (Table representation)
		{
			InnerTable = representation;
		}

		/// <summary>
		/// Returns a new TableBuilder with the table under-construction equal to
		/// the current InnerTable field.
		/// </summary>
		private TableBuilder Reference()
		{
			return new TableBuilder (InnerTable);
		}

		/// <summary>
		/// Changes the name of the internal table
		/// </summary>
		/// <param name="title">The title to change it to</param>
		public TableBuilder Title(string title)
		{
			InnerTable.Title = title;
			return Reference ();
		}

		/// <summary>
		/// Adds the column to the table
		/// </summary>
		/// <returns>The TableBuilder with the added column</returns>
		/// <param name="c">The column that will be added to the table</param>
		public TableBuilder AddColumn(Column c)
		{
			InnerTable.InsertColumn (c, 0);
			return Reference ();
		}

		/// <summary>
		/// Inserts a column at the given index
		/// </summary>
		/// <returns>The TableBuilder with the added column</returns>
		/// <param name="c">The column to add to the list</param>
		/// <param name="index">The index</param>
		public TableBuilder AddColumn(Column c, int index)
		{
			InnerTable.InsertColumn (c, index);
			return Reference ();
		}

		/// <summary>
		/// Finalizes and returns the inner table
		/// </summary>
		public Table Finalize()
		{
			return InnerTable;
		}
	}
}

