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
		public static IEnumerable<W> ForEvery<T, W>(IEnumerable<T> list, Transformer<T, W> t)
		{
			foreach (T item in list)
				yield return t (item);
			yield break;
		}
	}
}

