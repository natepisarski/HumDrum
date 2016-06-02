﻿using System;
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
		/// Fold the specified list to the right
		/// </summary>
		/// <param name="list">The list to fold</param>
		/// <param name="collapsor">The function to fold the list with</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="W">The 2nd type parameter.</typeparam>
		public static T Collapse<T>(this IEnumerable<T> list, Func<T, T, T> collapsor)
		{
			T currentValue = list.Get (0);

			for (int i = 1; i < list.Length (); i++) {
				currentValue = collapsor (currentValue, list.Get(i));
			}

			return currentValue;
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

		/// <summary>
		/// Generate a sequence of values based on a function that turns the 
		/// previous step into the next step.
		/// </summary>
		/// <param name="starting">The starting value.</param>
		/// <param name="steps">How many steps to take it, with 0 making a list of 1 element</param>
		/// <param name="function">The function to generate the list with</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		private static IEnumerable<T> GenerateReplace<T>(T starting, int steps, Func<T, T> function)
		{
			if (steps < 0)
				return new List<T> ();
			
			if (steps == 0)
				return Transformations.Wrap (function (starting));

			return Transformations.Concatenate (
				Transformations.Wrap (function (starting)),
				HigherOrder.GenerateReplace (function (starting), steps - 1, function));
					
		}

		/// <summary>
		/// Generate a sequence of values based on a function that turns the 
		/// previous step into the next step.
		/// </summary>
		/// <param name="starting">The starting value.</param>
		/// <param name="steps">How many steps to take it, with 0 making a list of 1 element</param>
		/// <param name="function">The function to generate the list with</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> Generate<T>(T starting, int steps, Func<T, T> function)
		{
			return Transformations.Concatenate (Transformations.Wrap(starting),
				GenerateReplace (starting, steps - 1, function));
		}
	}
}

