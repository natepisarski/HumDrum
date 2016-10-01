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
		void IntoFile (string filename);
		Database FromFile(string filename);
	}
}

