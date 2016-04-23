using System;
using System.Collections.Generic;
namespace HumDrum.Collections
{
	/// <summary>
	/// Static functions which relate to gathering information
	/// about a sequence.
	/// </summary>
	public static class Information
	{
		/// <summary>
		/// Gets the number of elements in this IEnumerable
		/// </summary>
		/// <param name="list">The list to count on</param>
		/// <typeparam name="T">The type parameter</typeparam>
		public static int Length<T>(this IEnumerable<T> list){
			if (list == null)
				return 0;
			
			int counter = 0;

			foreach (T item in list) 
				counter++;

			return counter;			
		}

		/// <summary>
		/// Get the item at the index from the list.
		/// </summary>
		/// <param name="list">The list</param>
		/// <param name="index">The index (0-based)</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Get<T>(this IEnumerable<T> list, int index){
			if(list.Length() == 0)
				return default(T);

			int counter = 0;

			foreach (T item in list) {
				if (counter == index)
					return item;
				else
					counter++;
			}
			throw new Exception ("Array Index out of bounds. Index: " + index + ". Array length: " + list.Length ());
		}


		/// <summary>
		/// Tests to see if two lists of the same type are equal based on each individual
		/// element's specification of equality.
		/// </summary>
		/// <param name="list1">The first list</param>
		/// <param name="list2">The second list</param>
		/// <typeparam name="T">A Generic type parameter</typeparam>
		public static bool Equal<T>(IEnumerable<T> list1, IEnumerable<T> list2)
		{
			// If the lists are no of equal length, they're obviously not the same.
			if (!(list1.Length ().Equals (list2.Length ())))
				return false;

			for (int i = 0; i < list1.Length () && i < list2.Length (); i++) {
				if (!(list1.Get (i).Equals (list2.Get (i))))
					return false;
			}

			return true;
		}

		/// <summary>
		/// Checks the number of times that an element occurs within
		/// a given sequence.
		/// </summary>
		/// <param name="list">The list </param>
		/// <param name="item">The item</param>
		/// <typeparam name="T">The generic type parameter</typeparam>
		public static int Times<T>(this IEnumerable<T> list, T item)
		{
			return HigherOrder.When (list, (T x) => x.Equals (item)).Length<T>();
		} 

		/// <summary>
		/// Determines whether this list has the specified item.
		/// </summary>
		/// <returns><c>true</c> if this instance has list item; otherwise, <c>false</c>.</returns>
		/// <param name="list">The lsit to check</param>
		/// <param name="item">The item to search for in the list</param>
		/// <typeparam name="T">The generic type parameter</typeparam>
		public static bool Has<T>(this IEnumerable<T> list, T item)
		{
			foreach (T listItem in list)
				if (listItem.Equals (item))
					return true;
			return false;
		}

		/// <summary>
		/// Turn any type of collection into an IEnumerable
		/// </summary>
		/// <param name="list">The list to genericize</param>
		/// <typeparam name="T">The type parameter of this list</typeparam>
		public static IEnumerable<T> Genericize<T>(this IEnumerable<T> list)
		{
			foreach (T item in list) 
				yield return item;
			
			yield break;
		}
	}
}

