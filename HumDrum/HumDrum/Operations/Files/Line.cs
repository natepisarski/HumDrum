using System;
using System.Collections.Generic;
using System.IO;

namespace HumDrum.Operations.Files
{
	/// <summary>
	/// Class representing a line from a text file
	/// </summary>
	public class Line
	{
		/// <summary>
		/// The text of the line
		/// </summary>
		/// <value>The text</value>
		public string Text {get; set;}

		/// <summary>
		/// The file where this line came from
		/// </summary>
		/// <value>The filename</value>
		public string Filename { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Files.Line"/> class.
		/// This uses the text from a line and the filename
		/// </summary>
		/// <param name="text">The text</param>
		/// <param name="file">The filename</param>
		public Line (string text, string file)
		{
			Text = text;
			Filename = file;
		}

		/// <summary>
		/// Reads the lines of a file and returns a list of Line objects.
		/// </summary>
		/// <returns>The lines objects</returns>
		/// <param name="filename">The filename to take in</param>
		public static IEnumerable<Line> AllLines(string filename)
		{
			foreach (string line in File.ReadAllLines(filename))
				yield return new Line (line, filename);
			yield break;
		}
	}
}

