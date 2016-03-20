using System;

namespace HumDrum.Operations.Files
{
	/// <summary>
	/// Sequential writer. This will return a filename
	/// that is sequential in some way.
	/// </summary>
	public interface ISequentialWriter
	{
		/// <summary>
		/// Gets the next possible name in the directory
		/// </summary>
		/// <param name="directory">The directory to analyze</param>
		string Name(string directory, string extension);

		/// <summary>
		/// Write the text to the next file in the directory
		/// </summary>
		/// <param name="directory">The directory to write in</param>
		/// <param name="text">The text to write</param>
		void Write(string directory, string text, string extension);
	}
}

