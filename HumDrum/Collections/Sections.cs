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
		/// <param name="delim">The character defining partitions</param>
		public static string[] ParseSections(string text, char delim){

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
				if (collecting && c.Equals (delim)) {
					collection.Add (selection);
					selection = "";
					collecting = false;
					continue;
				} 

				// Start collecting this selection
				if (!collecting && c.Equals (delim)) {
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

