using System;
using System.Collections.Generic;

namespace HumDrum.Collections
{
	/// <summary>
	/// Sections is a class for parsing collections which are separated with delimiters.
	/// It could reasonably be used in places where preserving whitespace is important.
	/// </summary>
	public static class Sections
	{
		//TODO: I don't see a need to make this anything but strings. But make it so that
		//TODO: delim is startDelim and endDelim

		/// <summary>
		/// Parses a section.
		/// Example: { these are words. |this is a string|. |this has a \| in it |
		/// </summary>
		/// <returns>The sections.</returns>
		/// <param name="text">The text to partition</param>
		/// <param name="startDelim">The character defining partitions</param>
		/// <param name="endDelim"> The character defining the end of the partitions</param>
		public static string[] ParseSections(string text, char startDelim, char endDelim){

			// What is being collected right now
			string selection = "";

			// Test to see if we need to find another | to flush selection
			bool collecting = false;

			// If the escape character \ appears, the next character is added to the selection
			bool escaped = false;

			// The final collection
			var collection = new List<string> ();

			foreach(char c in text){

				// Escaped? Add this character no matter what
				if (escaped) {
					escaped = false;
					selection += c;
					continue;
				}

				// This is an escape character? Escape the next one
				if (c.Equals ('\\')) {
					escaped = true;
					continue;
				}

				// Stop collecting this selection
				if (collecting && c.Equals (endDelim)) {
					collection.Add (selection);
					selection = "";
					collecting = false;
					continue;
				} 

				// Start collecting this selection
				if (!collecting && c.Equals (startDelim)) {
					collecting = true;
					continue;
				}

				// Words done? Clear selection. Otherwise, add the char.
				if (c.Equals (' ') && !collecting) {
					collection.Add (selection);
					selection = "";
				} else
					selection += c;
			}

			// Remove any empty strings that this found.
			collection.RemoveAll (x => x.Equals (""));

			return collection.ToArray ();
		}

		/// <summary>
		/// Parses the sections using one delimiter
		/// </summary>
		/// <returns>The sections</returns>
		/// <param name="text">Text.</param>
		/// <param name="delim">The delimiter</param>
		public static string[] ParseSections(string text, char delim){
			return ParseSections (text, delim, delim);
		}

		/// <summary>
		/// Split text, listening to the escape character.
		/// </summary>
		/// <returns>The split.</returns>
		/// <param name="text">The text to split on.</param>
		/// <param name="splitOn">The character to split this string on.</param>
		public static List<string> EscapeSplit(string text, char splitOn){
			// The total collection
			var collection = new List<string> ();

			// The current selection
			string selection = "";

			// Whether or not the escape character preceded this
			bool escaped = false;

			foreach (char c in text) {

				//Escaped? Add it no matter what.
				if (escaped) {
					escaped = false;
					selection += c;
					continue;
				}

				//Escape character? Ignore it an enable escaped.
				if (c.Equals ('\\')) {
					escaped = true;
					continue;
				}

				// Found your delimiter? Push the buffer and start collecting again
				if (c.Equals (splitOn)) {
					collection.Add (selection);
					selection = "";
					continue;
				}

				// Anything else? Push to the buffer
				selection += c;
			}

			// Push any last-minute data
			collection.Add (selection);

			collection.RemoveAll (x => x.Equals (""));

			return collection;
		}

		/// <summary>
		/// Extract the text from the inside of two delimiters,
		/// not including them. 
		/// </summary>
		/// <param name="text">The text to extract</param>
		/// <param name="startDelim">The starting delimiter</param>
		/// <param name="endDelim">The ending delmiter</param>
		public static string Internal(string text, char startDelim, char endDelim){
			// Count of how many startDelims vs endDelims there have been
			int count = 0;

			// A collection of the internal text
			string collection = "";

			foreach (char c in text) {


				if (c.Equals (startDelim)) {
					if (count > 0)
						collection += c;

					count++;
					continue;
				} else if (c.Equals (endDelim) && count > 0) {
					count--;

					if (count > 0)
						collection += c;
					continue;
				} else if (count > 0)
					collection += c;

			}

			return collection;
		}

		/// <summary>
		/// Return the "globs" of information from the text. This is a whitespace split
		/// with scope acting as a continuation of its text.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="startDelim">Start delim.</param>
		/// <param name="endDelim">End delim.</param>
		public static List<String> Globs(string text, char startDelim, char endDelim)
		{
			var local = new List<string> ();
			string buffer = "";

			bool escaped = false;
			bool collecting = false;

			foreach (char c in text) {
				
				if (c.Equals ('\\')) {
					escaped = true;
					continue;
				}

				if (escaped) {
					buffer += c;
					escaped = false;
					continue;
				}

				// If it's whitespace, that's the end of the word. Unless we're collecting
				if (!collecting && c.Equals (' ')) {
					local.Add (buffer);
					buffer = "";
					continue;
				}

				if (collecting && c.Equals (endDelim))
					collecting = false;

				if (c.Equals (startDelim))
					collecting = true;

					buffer += c;
			}

			local.Add (buffer);

			return local;
		}

		/// <summary>
		/// Turn an array of strings back into the original string
		/// </summary>
		/// <param name="toRepair">The array of strings to flatten</param>
		public static string RepairString(string[] toRepair){
			string collection = "";

			foreach(string item in toRepair){
				collection += item;
				collection += " ";
			}

			return collection.TrimEnd(' ');
		}
	}
}

