using System;
using System.Collections.Generic;

namespace HumDrum.Collections
{
	public static class Transformations
	{
		/// <summary>
		/// Gets the number of elements in this IEnumerable
		/// </summary>
		/// <param name="list">The list to count on</param>
		/// <typeparam name="T">The type parameter</typeparam>
		public static int Length<T>(this IEnumerable<T> list){
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
		/// Use varargs to create an array literal
		/// </summary>
		/// <param name="items">The items to add to the array</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T[] Make<T>(params T[] items){
			return items;
		}

		public static List<T> MakeList<T>(params T[] items){
			return new List<T> (items);
		}

		/// <summary>
		/// Create a subsequence of the given collection.
		/// </summary>
		/// <param name="list">The list to take the subsequence of</param>
		/// <param name="start">Where to start collecting</param>
		/// <param name="length">How many items to take</param>
		/// <typeparam name="T">T - type</typeparam>
		public static List<T> Subsequence<T>(IEnumerable<T> list, int start, int length){
			var returnCollection = new List<T> ();

			for (int i = start; i < list.Length (); i++)
				returnCollection.Add (list.Get (i));

			return returnCollection;
		}
	}
}

