using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Contains a list of columns
	/// </summary>
	public class Table
	{
		/// <summary>
		/// The columns of this row, with the type listed as a parameter to the tuple, and then the 
		/// actual sequence
		/// </summary>
		/// <value>The columns.</value>
		public List<Column> Columns {get; set;}

		/// <summary>
		/// Gets or sets the title of this table
		/// </summary>
		/// <value>The title for this table</value>
		public string Title {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Database.Table"/> class.
		/// This will simply name the table
		/// </summary>
		public Table (string tableName)
		{
			Columns = new List<Column> ();
			Title = tableName;
		}

		/// <summary>
		/// Returns the column names that this Table contains
		/// </summary>
		/// <value>The column names</value>
		public IEnumerable<string> ColumnNames() {
			foreach (Column c in Columns)
				yield return c.Title;
			yield break;
		}

		/// <summary>
		/// Returns the column at the given index
		/// </summary>
		/// <returns>The <see cref="HumDrum.Database.Column"/>.</returns>
		/// <param name="index">The index to return the column from</param>
		public Column ColumnAt(int index)
		{
			return Columns.Get (index);
		}


		/// <summary>
		/// Gets the item at the column and row specified
		/// </summary>
		/// <returns>The <see cref="``0 (owner=[Method HumDrum.Database.Table.ItemAt``1(col:System.Int32, row:System.Int32):``0])"/>.</returns>
		/// <param name="col">The column to return the information from</param>
		/// <param name="row">The row to return the information from</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T ItemAt<T>(int col, int row)
		{
			return ColumnAt (col).ItemAt<T> (row);
		}

		/// <summary>
		/// Returns the given row from the table. This will return
		/// a list where each item is a tuple of an object, and the type that
		/// the object is, so that it can be cast.
		/// </summary>
		/// <returns>The row at the given index</returns>
		/// <param name="index">The index, starting with 0, of the row you would like to return</param>
		public IEnumerable<Tuple<Object, Type>> GetRow(int index)
		{
			// TODO: Change to Row type
			foreach (Column c in Columns)
				yield return new Tuple<Object, Type> (c.ItemAt<Object> (index), c.ColumnType);
			yield break;
		}
			
		/// <summary>
		/// Inserts a column at the given place in the table
		/// </summary>
		/// <param name="c">The column to add to the table</param>
		/// <param name="position">The position that this column will go</param>
		public void InsertColumn(Column c, int position)
		{
			Columns.Insert (position, c);
		}

		/// <summary>
		/// Gets the column by the given name
		/// </summary>
		/// <returns>The column.</returns>
		/// <param name="columnTitle">Column title.</param>
		public Column GetColumn(string columnTitle)
		{
			foreach (Column c in Columns)
				if (c.Title.Equals (columnTitle))
					return c;
			throw new Exception ("Column with the given name {" + columnTitle + "}was not found in the database");
		}
			
	}
}

