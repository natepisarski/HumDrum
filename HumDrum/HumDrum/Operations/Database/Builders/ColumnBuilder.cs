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
}

