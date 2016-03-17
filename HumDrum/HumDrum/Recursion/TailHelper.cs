using System;
using System.Reflection;
using System.Collections.Generic;

namespace HumDrum.Recursion
{
	public static class TailHelper
	{
		/// <summary>
		/// Wraps the specified item in some type of class
		/// </summary>
		/// <param name="item">The name of the item to wrap</param>
		/// <typeparam name="T">The 1st type parameter to wrap</typeparam>
		public static List<T> Wrap<T>(T item){
			List<T> list = new List<T> ();
			list.Add (item);
			return list;
		}

		/// <summary>
		/// Concatenate two lists
		/// </summary>
		/// <param name="list1">The first list</param>
		/// <param name="list2">The second list</param>
		/// <typeparam name="T">Any type t</typeparam>
		public static IEnumerable<T> Concatenate<T>(IEnumerable<T> list1, IEnumerable<T> list2){
			foreach (T item in list1)
				yield return item;
			foreach (T item in list2)
				yield return item;
			yield break;
		}

		/// <summary>
		/// "Tack" the item onto an existing IEnumerable list
		/// </summary>
		/// <param name="list">The list</param>
		/// <param name="item">The item</param>
		/// <typeparam name="T">The type of both parameters</typeparam>
		public static IEnumerable<T> Tack<T>(this IEnumerable<T> list, T item)
		{
			return Concatenate (list, TailHelper.Wrap<T>(item));
		}
	}
}

