using System;
using System.Collections.Generic;

namespace HumDrum.Recursion
{
	public static class TailHelper
	{
		/// <summary>
		/// Wraps the specified item in a list
		/// </summary>
		/// <param name="item">The name of the item to wrap</param>
		/// <typeparam name="T">The 1st type parameter to wrap</typeparam>
		public static List<T> Wrap<T>(T item){
			var local = new List<T> ();
			local.Add (item);
			return local;
		}

		/// <summary>
		/// Concatenate two lists
		/// </summary>
		/// <param name="list1">The first list</param>
		/// <param name="list2">The second list</param>
		/// <typeparam name="T">Any type t</typeparam>
		public static List<T> Concatenate<T>(IEnumerable<T> list1, IEnumerable<T> list2){
			var local = new List<T> ();

			local.AddRange (list1);
			local.AddRange (list2);

			return local;
		}

		public static List<T> Tack<T>(this IEnumerable<T> list, T item)
		{
			return Concatenate (list, Wrap (item));
		}
	}
}

