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
		/// Performs an action on a unique member of the list
		/// </summary>
		/// <returns>The item from the list with the function applied to it</returns>
		/// <param name="list">The list of information</param>
		/// <param name="predicate">The predicate which determines which item to use</param>
		/// <param name="function">The function to use to transform the item</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="W">The 2nd type parameter.</typeparam>
		public static W DoTo<T, W>(this IEnumerable<T> list, Predicate<T> predicate, Func<T, W> function)
		{
			foreach (T item in list)
				if (predicate (item))
					return function (item);

			return default(W);
		}

		/// <summary>
		/// Performs the same 
		/// </summary>
		/// <returns>The to sequence.</returns>
		/// <param name="list">List.</param>
		/// <param name="predicate">Predicate.</param>
		/// <param name="function">Function.</param>
		public static IEnumerable<W> DoToSequence<T, W>(this IEnumerable<T> list, Predicate<T> predicate, Func<T, W> function)
		{
			foreach (T item in list)
				if (predicate (item))
					yield return function (item);
			yield break;
		}

		/// <summary>
		/// Returns the first element of the list to yield true for the predicate
		/// </summary>
		/// <param name="list">The list to search</param>
		/// <param name="predicate">The predicate to use for filtering</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T First<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			return HigherOrder.When (list, predicate).Get (0);
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
		/// Reduces a function over the list. Will use the first item of the list an an
		/// initial value for the first value.
		/// </summary>
		/// <param name="list">The list to reduce</param>
		/// <param name="function">The function</param>
		/// <param name="initialState">The initial state of the reduction</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Reduce<T>(this IEnumerable<T> list, Func<T, T, T> function, T initialState)
		{
			if (list.Length ().Equals (0))
				return initialState;
			else
				return Reduce<T> (list.Tail (), function, function (initialState, list.Head ()));
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
