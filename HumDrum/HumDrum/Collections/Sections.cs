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
		/// <summary>
		/// Parses Sections from a collection. "Sections" are subsequences of a greater
		/// collection which are noted with delimiters. For instance, "in this string |all of this is one|",
		/// with the startDelim and endDelim of |, "all of this is one" is parsed as one string. This is useful for
		/// applications where multiple words with whitespace must be passed in as one string, such as with command-line arguments.
		/// </summary>
		/// <returns>The sections, either defined by whitespace or delimiters</returns>
		/// <param name="sequence">The sequence, sans start and end delimiters</param>
		/// <param name="startDelim">The marker for the beginning of a section</param>
		/// <param name="endDelim">The marker for the end of a section</param>
		/// <param name="separator">The seperator for this sequence</param>
		/// <param name="escape">Optionally, an escape </param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<IEnumerable<T>> ParseSections<T>(IEnumerable<T> sequence, Predicate<T> startDelim, Predicate<T> endDelim, Predicate<T> separator, Predicate<T> escape = null){

			// What is being collected right now
			List<T> selection = new List<T>();

			// Test to see if we need to find another | to flush selection
			bool collecting = false;

			// If the escape character \ appears, the next character is added to the selection
			bool escaped = false;

			// The final collection
			var collection = new List<IEnumerable<T>> ();

			foreach(T c in sequence){

				// Escaped? Add this character no matter what
				if (escaped) {
					escaped = false;
					selection.Add(c);
					continue;
				}

				// This is an escape character? Escape the next one if escaping is enabled
				if (escape != null) {
					if (escape(c)) {
						escaped = true;
						continue;
					}
				}

				// Stop collecting this selection
				if (collecting && endDelim(c)) {
					collection.Add (selection);
					selection = new List<T> ();
					collecting = false;
					continue;
				} 

				// Start collecting this selection
				if (!collecting && startDelim(c)) {
					collecting = true;
					continue;
				}

				// Words done? Clear selection. Otherwise, add the char.
				if (separator(c) && !collecting) {
					collection.Add (selection);
					selection = new List<T> ();
				} else
					selection.Add (c);
			}

			// Remove any empty strings that this found.
			collection.RemoveAll (x => x.Equals (new List<T>()));

			return collection;
		}

		/// <summary>
		/// Implementation of ParseSections for strings. Assumes that spaces are the 
		/// separator and \ is the escape character.
		/// </summary>
		/// <returns>The sections parsed from the string</returns>
		/// <param name="text">The string of text itself</param>
		/// <param name="startDelim">The start delimiter for a string</param>
		/// <param name="endDelim">The ending delimiter</param>
		public static IEnumerable<string> ParseSections(string text, char startDelim, char endDelim){
			IEnumerable<IEnumerable<char>> result = ParseSections (
				                    text.ToCharArray (),
				                    Predicates.GenerateEqualityPredicate (startDelim),
				                    Predicates.GenerateEqualityPredicate (endDelim),
				                    Predicates.GenerateEqualityPredicate (' '),
				                    Predicates.GenerateEqualityPredicate ('\\'));
			
			foreach (IEnumerable<char> section in result)
				yield return Information.AsString (section);
			
			yield break;
		}

		/// <summary>
		/// Parses the sections using one delimiter
		/// </summary>
		/// <returns>The sections</returns>
		/// <param name="text">The text</param>
		/// <param name="delim">The delimiter</param>
		public static IEnumerable<string> ParseSections(string text, char delim){
			return ParseSections (text, delim, delim);
		}

		/// <summary>
		/// Partition the sequence, listening to the predicate.
		/// </summary>
		/// <returns>Text, split using a predicate</returns>
		/// <param name="text">The text to split on.</param>
		/// <param name="splitOn">The character to split this string on.</param>
		public static IEnumerable<IEnumerable<T>> EscapeSplit<T>(IEnumerable<T> sequence, Predicate<T> splitOn, Predicate<T> escape = null){
			
			// The total collection
			var collection = new List<List<T>> ();

			// The current selection
			List<T> selection = new List<T>();

			// Whether or not the escape character preceded this
			bool escaped = false;

			foreach (T c in sequence) {

				//Escaped? Add it no matter what.
				if (escaped) {
					escaped = false;
					selection.Add(c);
					continue;
				}

				//Escape character? Ignore it an enable escaped.
				if (escape != null) {
					if (escape (c)) {
						escaped = true;
						continue;
					}
				}

				// Found your delimiter? Push the buffer and start collecting again
				if (splitOn(c)) {
					collection.Add (selection);
					selection = new List<T> ();
					continue;
				}

				// Anything else? Push to the buffer
				selection.Add(c);
			}

			// Push any last-minute data
			collection.Add (selection);

			collection.RemoveAll (x => x.Equals (new List<T>()));

			return collection.Genericize();
		}

		/// <summary>
		/// Partition the sequence, listening to the predicate.
		/// </summary>
		/// <returns>The split.</returns>
		/// <param name="text">The text to split on.</param>
		/// <param name="splitOn">The character to split this string on.</param>
		public static IEnumerable<string> EscapeSplit(string text, IEnumerable<char> splitOn){
			return EscapeSplit (text.ToCharArray (),
				Predicates.GenerateEqualityPredicate (splitOn),
				Predicates.GenerateEqualityPredicate ('\\')).ForEvery ((IEnumerable<char> x) => Information.AsString (x));
		}

		/// <summary>
		/// Performs an EscapeSplit with just one delimiter.
		/// </summary>
		/// <returns>The varying sections</returns>
		/// <param name="text">The text to split up</param>
		/// <param name="splitOn">The character to split on</param>
		public static IEnumerable<string> EscapeSplit(string text, char splitOn)
		{
			return EscapeSplit (text, Transformations.Make (splitOn));
		}

		/// <summary>
		/// Finds the part of a sequence contained by two predicates.
		/// </summary>
		/// <param name="sequence">The sequence to look in</param>
		/// <param name="startDelim">The starting delimiter</param>
		/// <param name="endDelim">The ending delimiter</param>
		/// <param name="escape">Optional escape predicate</param>
		/// <typeparam name="T">The type of this sequence</typeparam>
		public static IEnumerable<IEnumerable<T>> Internal<T>(IEnumerable<T> sequence, Predicate<T> startDelim, Predicate<T> endDelim, Predicate<T> escape = null){
			
			// Count of how many startDelims vs endDelims there have been
			int count = 0;

			// Since more than one scope can be found, collections must be "pushed" here.
			List<List<T>> allCollections = new List<List<T>> ();

			// A collection of the internal text
			List<T> collection = new List<T>();

			// Determines whether or not this iteration is escaped
			bool escaped = false;

			foreach (T c in sequence) {

				// Escaped? Add this regardless, change no counts
				if (escaped) {
					escaped = false;
					collection.Add (c);
					continue;
				}

				// Is there an escape predicate? Does this iteration trip it?
				if (escape != null && escape (c)) {
					escaped = true;
					continue;
				}

				// Is this the startDelimiter? If it's not the first, add it.
				if (startDelim(c)) {
					if (count > 0)
						collection.Add(c);
					
					// We are nested one level deeper now
					count++;
					continue;

				} // Already collecting? See if it's time to end
				else if (endDelim(c) && count > 0) {
					// Go a level up
					count--;

					if (count > 0)
						collection.Add (c);
					else {
						allCollections.Add (collection);
						collection = new List<T> ();
						count = 0;
						continue;
					}// Last delimiter? Return
				} else if (count > 0)
					collection.Add(c);

			}

			return allCollections;
		}

		/// <summary>
		/// Extract the text from the inside of two delimiters,
		/// not including them. 
		/// </summary>
		/// <param name="text">The text to extract</param>
		/// <param name="startDelim">The starting delimiter</param>
		/// <param name="endDelim">The ending delmiter</param>
		public static IEnumerable<string> Internal(string text, char startDelim, char endDelim){
			return 
					Sections.Internal (
						text.ToCharArray (), 
						Predicates.GenerateEqualityPredicate (startDelim),
						Predicates.GenerateEqualityPredicate (endDelim),
					Predicates.GenerateEqualityPredicate ('\\')).ForEvery(x => Information.AsString(x));
		}

		/// <summary>
		/// Wrapper for Internal that only returns the first scope found
		/// </summary>
		/// <param name="text">The text to analyze</param>
		/// <param name="startDelim">The starting delimiter</param>
		/// <param name="endDelim">The ending delimiter</param>
		public static string FirstInternal(string text, char startDelim, char endDelim)
		{
			return Internal (text, startDelim, endDelim).Head ();
		}

		/// <summary>
		/// Returns "Globs" of information from a sequence. Given starting and ending delimiters and a separator,
		/// this function will return the sections which are split using the separator unless it falls between the 
		/// starting and ending delimiters
		/// </summary>
		/// <param name="sequence">The sequence to analyize the globs of</param>
		/// <param name="startDelim">The predicate acting as the starting delimiter</param>
		/// <param name="endDelim">End delim.</param>
		/// <param name="separator">Separator.</param>
		/// <param name="escape">Escape.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<IEnumerable<T>> Globs<T>(IEnumerable<T> sequence, Predicate<T> startDelim, Predicate<T> endDelim, Predicate<T> separator, Predicate<T> escape = null)
		{
			var collection = new List<List<T>> ();
			List<T> buffer = new List<T> ();

			bool escaped = false;
			bool collecting = false;

			foreach (T c in sequence) {

				if (escaped) {
					buffer.Add(c);
					escaped = false;
					continue;
				}

				if (escape != null && escape(c)) {
					escaped = true;
					continue;
				}



				// If it's whitespace, that's the end of the word. Unless we're collecting
				if (!collecting && separator(c)) {
					collection.Add (buffer);
					buffer = new List<T>();
					continue;
				}

				if (collecting && endDelim(c))
					collecting = false;

				if (startDelim(c))
					collecting = true;

				buffer.Add(c);
			}

			collection.Add (buffer);

			return collection.Genericize ();
		}
			
		public static IEnumerable<string> Globs(string text, char startDelim, char endDelim)
		{
			return Sections.Globs (
				text.ToCharArray (),
				Predicates.GenerateEqualityPredicate (startDelim),
				Predicates.GenerateEqualityPredicate (endDelim),
				Predicates.GenerateEqualityPredicate (' '),
				Predicates.GenerateEqualityPredicate ('\\')).ForEvery(x => Information.AsString(x));
		}

		/// <summary>
		/// Attempts to repair a sequence by flattening it one level.
		/// </summary>
		/// <returns>The sequence to repair</returns>
		/// <param name="sequence">The flattened sequence</param>
		/// <typeparam name="T">The type of this sequence</typeparam>
		public static IEnumerable<T> RepairSequence<T>(IEnumerable<IEnumerable<T>> sequence)
		{
			List<T> flattened = new List<T> ();

			foreach (IEnumerable<T> innerList in sequence) {
				flattened.AddRange (innerList);
			}

			return flattened.Genericize ();
		}

		/// <summary>
		/// Flattens the nested list, but also intersperses the items with the item list.
		/// </summary>
		/// <returns>The repaired sequence</returns>
		/// <param name="sequence">Sequence.</param>
		/// <param name="item">Item.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> RepairSequenceWith<T>(IEnumerable<IEnumerable<T>> sequence, IEnumerable<T> item)
		{
			List<T> flattened = new List<T> ();

			foreach (IEnumerable<T> innerList in sequence) {
				flattened.AddRange (innerList);
				flattened.AddRange (item);
			}

			return Transformations.Subsequence (flattened, 0, flattened.Length () - item.Length ()); // Drops the last item
		}

		/// <summary>
		/// Repairs the sequence by interspersing the inner list with the given item.
		/// </summary>
		/// <returns>The repaired sequence</returns>
		/// <param name="sequence">The nested list structure</param>
		/// <param name="item">The item to intersperse with</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> RepairSequenceWith<T>(IEnumerable<IEnumerable<T>> sequence, T item)
		{
			return RepairSequenceWith (sequence, Transformations.Wrap (item));
		}

		/// <summary>
		/// Turn an array of strings back into the original string
		/// </summary>
		/// <param name="toRepair">The array of strings to flatten</param>
		public static string RepairString(string[] toRepair){
			return Information.AsString(RepairSequence (HigherOrder.ForEvery (toRepair, x => x.ToCharArray ())));
		}

		/// <summary>
		/// Very generic string repair
		/// </summary>
		/// <returns>The string</returns>
		/// <param name="toRepair">The repaired string</param>
		public static string RepairString(IEnumerable<string> toRepair)
		{
			return RepairString (toRepair.AsArray ());
		}

		/// <summary>
		/// Repairs a string by interpolating it with a given string
		/// </summary>
		/// <returns>The string collection flattened, with "with" interpolated.</returns>
		/// <param name="toRepair">The string to repair</param>
		/// <param name="with">What to interpolate the string with</param>
		public static string RepairStringWith(IEnumerable<string> toRepair, string with)
		{
			string words = "";

			foreach (string word in toRepair)
				words += (word + with);

			return Information.AsString(words.Subsequence(0, words.Length - with.Length));
		}

		/// <summary>
		/// Takes a collection of strings and concatenates them into one
		/// </summary>
		/// <returns>The string</returns>
		/// <param name="source">The strings</param>
		public static string DisrepairString(IEnumerable<string> source) {
			string localString = "";

			foreach (string s in source)
				localString += (s + " ");

			return localString;
		}
	}
}

