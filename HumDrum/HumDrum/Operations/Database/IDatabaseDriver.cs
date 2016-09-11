using System;
using System.Collections.Generic;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Defines a database driver.
	/// This can interact with an existing database driver and return native HumDrum database types.
	/// </summary>
	public interface IDatabaseDriver
	{
		IEnumerable<Column> Select (Predicate<Column> criteria);

		void Remove (Predicate<Column> criteria);

		void InsertInto<T> (string columnName, T item);

		void InsertInto<T>(Predicate<Column> criteria, T item);

		void InsertInto (Column c);
	}
}

