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

		public IEnumerable<Row> Rows {
			get {
				for (int i = 0; i < Columns.Get (0).Data.Length() - 1; i++) {
					yield return new Row (this, i);
				}
				yield break;
			}
		}
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
		public Row GetRow(int index)
		{
			return new Row (this, index);
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

		/// <summary>
		/// Unconditionally adds a new colum to this table.
		/// </summary>
		/// <param name="column">The column to add to the table</param>
		public void AddColumn(Column column) 
		{
			Columns.Add (column);
		}

		/// <summary>
		/// Removes the column from the database
		/// </summary>
		/// <param name="columnPred">The criteria to base the column on</param>
		public void RemoveColumn(Predicate<Column> columnPred)
		{
			var newColumns = new List<Column> ();

			foreach (Column c in Columns) 
				if (!columnPred (c))
					newColumns.Add (c);

			Columns = newColumns;
		}

		/// <summary>
		/// Selects a list of columns from this table
		/// </summary>
		/// <returns>The columns which match the criteria</returns>
		/// <param name="columnPred">The predicate that filters the columns</param>
		public IEnumerable<Column> SelectColumn(Predicate<Column> columnPred)
		{
			foreach (Column c in Columns)
				if (columnPred (c))
					yield return c;
			yield break;
		}

		/// <summary>
		/// Inserts an item into the database
		/// </summary>
		/// <param name="columnName">The column name in the table</param>
		/// <param name="item">The item to insert into the column</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void InsertInto<T>(string columnName, T item)
		{
			GetColumn (columnName).Insert<T> (item);
		}

		/// <summary>
		/// Inserts the item into all columns that match the critera
		/// </summary>
		/// <param name="criteria">The criteria by which the columns are selected</param>
		/// <param name="item">The item to insert into the columns</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void InsertInto<T>(Predicate<Column> criteria, T item)
		{
			var columns = SelectColumn (criteria);

			foreach (Column column in columns)
				column.Insert<T> (item);
		}

		/// <summary>
		/// Simply inserts a column into the database
		/// </summary>
		/// <param name="c">The column to insert into the column</param>
		public void InsertInto(Column c)
		{
			Columns.Add (c);
		}

		/// <summary>
		/// Gets the schema from this table
		/// </summary>
		/// <returns>The schema</returns>
		public Schema GetSchema() {
			return new Schema (this);
		}
			
		/// <summary>
		/// This will append columns which fit the given schema onto this table
		/// </summary>
		/// <param name="schema">The schema to use for this table</param>
		public void AppendSchema(Schema schema) {
			foreach (SchemaAtom atom in schema.TableSchema) {
				Columns.Add (
					new Column (atom.ColumnName, atom.ColumnType));
			}
		}

		/// <summary>
		/// Appends the row to the given table, given that the Row's schema type is compatible with this table's schema type.
		/// For appending a row, you do not need the Schema name to be the same.
		/// </summary>
		/// <param name="row">The row to append to the table</param>
		public void AppendRow(Row row /*Fight da Powa */) {
			// Type Check
			for (int i = 0; i < row.RowSchema.TableSchema.Length (); i++)
				if (!(Columns.Get (i).ColumnType.IsEquivalentTo (row.RowSchema.TableSchema.Get (i).ColumnType)))
					return;

			// Types check out, let's append the row now
			for (int i = 0; i < row.Items.Length (); i++) 
				Columns.Get (i).Insert<Object>(row.Items.Get (i).Item);
		}
	}
}

