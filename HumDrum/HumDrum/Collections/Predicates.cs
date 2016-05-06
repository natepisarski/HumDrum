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
			foreach (bool item in list)
				if (item)
					return true;
			return false;
		}

		/// <summary>
		/// Checks to see if the predicate holds true for any members of this list
		/// </summary>
		/// <param name="list">The list to check</param>
		/// <param name="p">The predicate to use</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static bool Any<T>(this IEnumerable<T> list, Predicate<T> p)
		{
			return Any (list.ForEvery (x => p (x)));
		}

		/// <summary>
		/// Will return true if all of the booleans in this list are true.
		/// </summary>
		/// <param name="list">The list of booleans to test</param>
		public static bool All(this IEnumerable<bool> list)
		{
			foreach (bool item in list)
				if (!item)
					return false;
			return true;
		}

		/// <summary>
		/// Test to see if this predicate holds true for every member
		/// of the list.
		/// </summary>
		/// <param name="list">The list to test</param>
		/// <param name="predicate">The predicate to test with</param>
		public static bool All<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			return All (list.ForEvery (x => predicate (x)));
		}

		/// <summary>
		/// Creates a predicate that checks for equality against a certain member.
		/// </summary>
		/// <returns>The equality predicate</returns>
		/// <param name="equalTo">The member to check equality against</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static Predicate<T> GenerateEqualityPredicate<T>(T equalTo)
		{
			return (x => x.Equals (equalTo));
		}

		/// <summary>
		/// Creates a predicate that checks to see if something is equal to
		/// any of the items in the list
		/// </summary>
		/// <returns>The generated predicate</returns>
		/// <param name="equalTo">A list of things to check equality against</param>
		/// <typeparam name="T">The type parameter</typeparam>
		public static Predicate<T> GenerateEqualityPredicate<T>(this IEnumerable<T> equalTo)
		{
			return (x => equalTo.Has (x));
		}

		/// <summary>
		/// Returns a Tautology, a predicare which always returns true.
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		public static Predicate<T> Tautology<T>()
		{
			return x => true;
		}

		/// <summary>
		/// Returns a contradiction, a predicate which always returns false.
		/// </summary>
		/// <typeparam name="T">The type the contradiction works with this type</typeparam>
		public static Predicate<T> Contradiction<T>()
		{
			return x => false;
		}
	}
}
