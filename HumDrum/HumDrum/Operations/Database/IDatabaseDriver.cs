using System;
using System.Collections.Generic;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// Defines a database driver.
	/// This can interact with an existing database driver and return native HumDrum database types.
	/// </summary>
	[Experimental]
	public interface IDatabaseDriver
	{
		/// <summary>
		/// Write a Database into a file in a format that can be read by this driver
		/// </summary>
		/// <param name="filename">The filename to write into</param>
		void IntoFile (string filename);

		/// <summary>
		/// Creates a Database from the given Filename
		/// </summary>
		/// <returns>The database object</returns>
		/// <param name="filename">The filename to read from</param>
		Database FromFile(string filename);
	}
}

