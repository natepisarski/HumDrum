using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Represents a named column in a database.
	/// </summary>
	public class Column
	{
		/// <summary>
		/// What is the name of this column?
		/// </summary>
		/// <value>The title for this column</value>
		public string Title {get; set;}

		/// <summary>
		/// The ordered data from this table
		/// </summary>
		/// <value>The data contained in this Column</value>
		public List<Object> Data { get; set; }

		/// <summary>
		/// The type of data contained in this column. This will be used for casts when
		/// referencing data in this table
		/// </summary>
		/// <value>The type of the column</value>
		public Type ColumnType { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.Column"/> class.
		/// This provides the entirety of this column, including initial data, title, and the type of the column.
		/// </summary>
		/// <param name="title">The name for this column</param>
		/// <param name="type">The type of data that this column has</param>
		/// <param name="data">The data itself</param>
		public Column (string title, Type type, IEnumerable<Object> data) : this(title, type) {
			Data = new List<Object> ();
			Data.AddRange (data);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Column`1"/> class.
		/// This will make the data empty, but name the column
		/// </summary>
		/// <param name="title">The name of this column in the database</param>
		/// <param name="type">The type that this column is going to be holding</param>
		public Column (string title, Type type) : this(title)
		{
			ColumnType = type;
		}

		/// <summary>
		/// Initializes a column without a type. If you use this method, make sure to use .initialize()
		/// </summary>
		/// <param name="title">The title of this column</param>
		public Column(string title)
		{
			Data = new List<Object> ();
			Title = title;
		}

		/// <summary>
		/// Initializes a new column using the given type and title
		/// </summary>
		/// <param name="title">The title of the column</param>
		/// <typeparam name="T">The type that this column will contain</typeparam>
		public static Column Initialize<T>(string title)
		{
			return new Column (title, typeof(T));
		}

		/// <summary>
		/// Checks that the given type is equivalent to the type that this column manages
		/// </summary>
		/// <returns><c>true</c>, if check was typed, <c>false</c> otherwise.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public bool TypeCheck<T>()
		{
			return (typeof(T).IsEquivalentTo (ColumnType) || typeof(T).IsEquivalentTo(typeof(Object)));
		}

		/// <summary>
		/// Gets the items located at the given position in the database
		/// </summary>
		/// <returns>The <see cref="`0 (owner=HumDrum.Column`1)"/>.</returns>
		/// <param name="position">The index, beginning with 0, of</param>
		public T ItemAt<T>(int position)
		{
			// If you're casting an object from a column it should always work, since the cast cannot fail
			if (typeof(T).IsEquivalentTo (ColumnType) || typeof(T) == typeof(Object))
				return (T)Data.Get (position);
			else
				throw new Exception ("The type provided does not match this column's type.");
		}

		/// <summary>
		/// Insert the item at the given position in this column, moving everything else one down
		/// </summary>
		/// <param name="position">The position to place the item at </param>
		/// <param name="item">The item to insert into the table </param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Insert<T>(T item, int position)
		{
			// Type check
			if (TypeCheck<T> ()) {
				Data.Insert (position, item);
			}
		}

		/// <summary>
		/// Inserts an item into the table at the end
		/// </summary>
		/// <param name="item">The item to insert into the column</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Insert<T>(T item)
		{
			// If this is an empty column, you don't want to try to put it at -1, thus the ternary.
			var insertionPoint = Data.Length () == 0 ? 0 : Data.Length () - 1;
			Insert<T> (item, insertionPoint);
		}

		/// <summary>
		/// Replaces the speicified index with the item
		/// </summary>
		/// <param name="item">The item to replace the index with</param>
		/// <param name="position">The position to insert the item into</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Replace<T>(T item, int position)
		{
			if (TypeCheck<T> ()) {
				Data.RemoveAt (position);
				Data.Insert (position, item);
			}
		}

		/// <summary>
		/// Removes an element from this column completely
		/// </summary>
		/// <param name="position">The index to remove the item on</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void Delete<T>(int position)
		{
			Data.RemoveAt (position);
		}
	}
}