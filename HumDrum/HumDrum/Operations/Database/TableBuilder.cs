using System;
using HumDrum.Collections;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Builds a column within the table given a certain type
	/// </summary>
	public class ColumnBuilder<T> {

		/// <summary>
		/// Returns a blank ColumnBuilder with the given type
		/// </summary>
		public static ColumnBuilder<T> Start()
		{
			return new ColumnBuilder<T> ();
		}

		/// <summary>
		/// The column that is being built
		/// </summary>
		/// <value>The inner column</value>
		public Column InnerColumn {get; set;}

		/// <summary>
		/// The default constructor, leaving the title of this column blank
		/// </summary>
		public ColumnBuilder()
		{
			InnerColumn = new Column ("", typeof(T));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Database.ColumnBuilder`1"/> class.
		/// This will initialize the type and name of the column
		/// </summary>
		/// <param name="title">Title.</param>
		public ColumnBuilder(string title) {
			InnerColumn = new Column (title, typeof(T));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Database.ColumnBuilder`1"/> class.
		/// This directly sets the internal representation of the column
		/// </summary>
		/// <param name="column">The column that this ColumnBuilder is working on</param>
		private ColumnBuilder(Column column)
		{
			InnerColumn = column;
		}

		/// <summary>
		/// Changes the title of the internal column
		/// </summary>
		/// <param name="title">The title to make the inner column</param>
		public ColumnBuilder<T> Title(string title)
		{
			InnerColumn.Title = title;
			return new ColumnBuilder<T> (InnerColumn);
		}

		/// <summary>
		/// Appends the data to the column
		/// </summary>
		/// <returns>The ColumnBuilder with the motifications made to it</returns>
		/// <param name="data">The data to add to the column</param>
		public ColumnBuilder<T> AddData(T data)
		{
			InnerColumn.Insert (data, InnerColumn.Data.Length());
			return new ColumnBuilder<T> (InnerColumn);
		}

		/// <summary>
		/// Inserts some data at a certain point in this column
		/// </summary>
		/// <returns>The data to add to the list</returns>
		/// <param name="data">The data to add to the list</param>
		/// <param name="position">The position to add the data into</param>
		public ColumnBuilder<T> AddData(T data, int position)
		{
			InnerColumn.Insert (data, position);
			return new ColumnBuilder<T> (InnerColumn);
		}

		/// <summary>
		/// Returns the column
		/// </summary>
		public Column Finalize()
		{
			return InnerColumn;
		}

	}

	/// <summary>
	/// The TableBuilder allows you to generate database tables easily.
	/// </summary>
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
		/// Changes the name of the internal table
		/// </summary>
		/// <param name="title">The title to change it to</param>
		public TableBuilder Title(string title)
		{
			InnerTable.Title = title;
			return new TableBuilder (InnerTable);
		}

		/// <summary>
		/// Adds the column to the table
		/// </summary>
		/// <returns>The TableBuilder with the added column</returns>
		/// <param name="c">The column that will be added to the table</param>
		public TableBuilder AddColumn(Column c)
		{
			InnerTable.InsertColumn (c, 0);
			return new TableBuilder (InnerTable);
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
			return new TableBuilder (InnerTable);
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

