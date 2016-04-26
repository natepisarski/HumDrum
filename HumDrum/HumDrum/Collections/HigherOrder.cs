using System;
using System.Collections.Generic;

namespace HumDrum.Collections
{
	public static class HigherOrder
	{
		public delegate W Transformer<T, W>(T actOn);

		/// <summary>
		/// Returns a list made only of elements where this predicate is true
		/// </summary>
		/// <param name="list">The list to analyze</param>
		/// <param name="pred">The predicate</param>
		/// <typeparam name="T">The generic type parameter</typeparam>
		public static IEnumerable<T> When<T>(this IEnumerable<T> list, Predicate<T> pred)
		{
			foreach (T item in list)
				if (pred (item))
					yield return item;
			yield break;
		}

		/// <summary>
		/// IEnumerable extension providing pure ForEach-like support.
		/// </summary>
		/// <returns>Every transformed element of the list</returns>
		/// <param name="list">The list</param>
		/// <param name="t">The transformer (lambda)</param>
		/// <typeparam name="T">The type of the list</typeparam>
		/// <typeparam name="W">The return type of the lambda</typeparam>
		public static IEnumerable<W> ForEvery<T, W>(this IEnumerable<T> list, Transformer<T, W> t)
		{
			foreach (T item in list)
				yield return t (item);
			yield break;
		}

		/// <summary>
		/// Do all of the elements in this list return true for the predicate?
		/// </summary>
		/// <returns><c>true</c>, if all was done, <c>false</c> otherwise.</returns>
		/// <param name="list">The list to test</param>
		/// <param name="pred">The predicate to test the list with</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static bool DoAll<T>(this IEnumerable<T> list, Predicate<T> pred)
		{
			foreach (T item in list) {
				if (!pred (item))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Collects elements of the list while the predicate is true, and includes
		/// the element that causes failure.
		/// </summary>
		/// <returns>The list</returns>
		/// <param name="list">The data to filter</param>
		/// <param name="predicate">The predicate used for filtering</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> WhileInclusive<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			// If it's true, collect it then stop collecting
			foreach (T item in list) {
				if (!predicate(item)) {
					yield return item;
					break;
				}

				yield return item;
			}

			yield break;
		}

		/// <summary>
		/// Collects the elements of the list after an element causes the predicate to be true.
		/// This includes the element which causes it.
		/// </summary>
		/// <returns>The trigger, and everything after it/returns>
		/// <param name="list">The list to filter</param>
		/// <param name="predicate">The predicate used for filtering</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> AfterInclusive<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			bool collecting = false;

			foreach (T item in list) {

				// If it's true, start collecting on it
				if (predicate (item)) {
					yield return item;
					collecting = true;
					continue;

				} else if (collecting)
					yield return item;
			}

			yield break;
		}

		/// <summary>
		/// Collects the elements of a list until the predicate is false.
		/// </summary>
		/// <param name="list">The list to filter</param>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> While<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var temp = HigherOrder.WhileInclusive (list, predicate);
			return temp.RemoveAt (temp.Length() - 1);
		}

		/// <summary>
		/// Once a predicate is true, return everything after it.
		/// </summary>
		/// <param name="list">The list to test</param>
		/// <param name="predicate">The predicate used for testing</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static IEnumerable<T> After<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var temp = HigherOrder.AfterInclusive (list, predicate);
			return temp.RemoveAt (0);
		}
	}
}

