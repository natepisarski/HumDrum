using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Collections
{
	/// <summary>
	/// Class for analyzing collections of data based on some predicate.
	/// </summary>
	public static class Predicates
	{
		/// <summary>
		/// Will return true if any of the booleans in the list is true.
		/// </summary>
		/// <param name="list">A list of booleans</param>
		public static bool Any(IEnumerable<bool> list)
		{
			if (list.Length() == 0)
				return true;
			else
				return list.Get<bool> (0) || Any (list.Tail ());
		}

		/// <summary>
		/// Checks to see if the predicate 
		/// </summary>
		/// <param name="list">List.</param>
		/// <param name="p">P.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool Any<T>(IEnumerable<T> list, Predicate<T> p)
		{
			foreach (T item in list)
				if (p (item))
					return true;
			return false;
		}
		/// <summary>
		/// Will return true if all of the booleans in this list are true.
		/// </summary>
		/// <param name="list">The list of booleans to test</param>
		public static bool All(IEnumerable<bool> list)
		{
			if (list.Length() == 0)
				return true;
			else
				return list.Get<bool> (0) && Any (list.Tail<bool> ());
		}
	}
}

