using System;
using System.IO;
using System.Collections.Generic;

namespace HumDrum.Operations.Files
{
	/// <summary>
	/// Numerical Writer is a SequentialWriter that deals
	/// with numbered files
	/// </summary>
	public class NumericalWriter : ISequentialWriter
	{
		/// <summary>
		/// Constructor. Doesn't have to do anything.
		/// </summary>
		public NumericalWriter()
		{

		}

		/// <summary>
		/// Attempt to find a number with the given extension that is not 
		/// currently present in this directory.
		/// </summary>
		/// <param name="directory">The directory to analyze</param>
		/// <param name="extension">The extension</param>
		public string Name(string directory, string extension)
		{
			List<string> filenames = new DirectorySearch (directory, SearchOption.TopDirectoryOnly).Files;

			for (int i = 0;; i++) {
				if (!(filenames.Contains (i + extension)))
					return (i + extension);
			}
			throw new Exception ("Infinite loop in NumericalWriter exited for unknown reason)");
		}

		/// <summary>
		/// Write the text to the next file in the directory
		/// </summary>
		/// <param name="directory">The directory to write in</param>
		/// <param name="text">The text to write</param>
		/// <param name="extension">Extension.</param>
		public void Write(string directory, string text, string extension)
		{
			File.WriteAllText (directory + "/" + Name (directory, extension), text);
		}
	}
}

