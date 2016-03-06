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
		public static List<T> Subsequence<T>(this IEnumerable<T> list, int start, int length){
			var returnCollection = new List<T> ();

			for (int i = start; length>0 && i < list.Length(); i++) {
				returnCollection.Add (list.Get (i));
				length--;
			}

			return returnCollection;
		}

		public static List<T> Tail<T>(this IEnumerable<T> list)
		{
			return Subsequence (list, 0, list.Length ());
		}
		/// <summary>
		/// Collects elements of the list while the predicate is true, and includes
		/// the element that causes failure.
		/// </summary>
		/// <returns>The list</returns>
		/// <param name="list">The data to filter</param>
		/// <param name="predicate">The predicate used for filtering</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> WhileInclusive<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var collected = new List<T> ();

			foreach (T item in list) {
				if (!predicate(item)) {
					collected.Add (item);
					break;
				}

				collected.Add (item);
			}

			return collected;
		}

		/// <summary>
		/// Collects the elements of the list after an element causes the predicate to be true.
		/// This includes the element which causes it.
		/// </summary>
		/// <returns>The trigger, and everything after it/returns>
		/// <param name="list">The list to filter</param>
		/// <param name="predicate">The predicate used for filtering</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> AfterInclusive<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var collected = new List<T> ();
			bool collecting = false;

			foreach (T item in list) {
				if (predicate (item)) {
					collected.Add (item);
					collecting = true;
					continue;
				} else if (collecting)
					collected.Add (item);
			}

			return collected;
		}

		/// <summary>
		/// Collects the elements of a list until the predicate is false.
		/// </summary>
		/// <param name="list">The list to filter</param>
		/// <param name="predicate">Predicate.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static List<T> While<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var temp = WhileInclusive (list, predicate);
			temp.RemoveAt (temp.Count - 1);

			return temp;
		}

		/// <summary>
		/// Once a predicate is true, return everything after it.
		/// </summary>
		/// <param name="list">The list to test</param>
		/// <param name="predicate">The predicate used for testing</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static List<T> After<T>(this IEnumerable<T> list, Predicate<T> predicate)
		{
			var temp = AfterInclusive (list, predicate);
			temp.RemoveAt (0);
			return temp;
		}

		/// <summary>
		/// Removes the duplicates from the list.
		/// </summary>
		/// <returns>The list, without any duplicates</returns>
		/// <param name="list">The list to scan</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static T[] RemoveDuplicates<T>(this IEnumerable<T> list)
		{
			var local = new List<T> ();

			foreach (T item in list) {
				if (!local.Contains (item))
					local.Add (item);
			}

			return local.ToArray ();
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
		/// Returns a sequence starting with some other sequence, and everything after it.
		/// As an example, in this sentence "the cat jumps over the dog", startingAt cat would
		/// return "cat jumps over the dog".
		/// </summary>
		/// <returns>The sequence beginning with the parameter</returns>
		/// <param name="sequence">The sequence to examine</param>
		/// <param name="beginning">The sequence to begin with</param>
		/// <typeparam name="T">The generic type parameter</typeparam>
		public static List<T> StartingWith<T>(IEnumerable<T> sequence, IEnumerable<T> beginning)
		{
			// The sequence cannot be less than "beginning", so the loop doesn't get that far.
			for (int i = 0; i < sequence.Length () - beginning.Length (); i++) {
				
				//An amount of text equal to the length of the beginning sequence
				var chunk = Transformations.Subsequence (sequence, i, beginning.Length ());

				if (Transformations.Equal (chunk, beginning))
					return Transformations.Subsequence (sequence, i, sequence.Length ());
			}

			// This sequence was not present in the list.
			return new List<T> ();
		}

		/// <summary>
		/// Returns the position where the sequence is found.
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="sequence">Sequence.</param>
		/// <param name="beginning">Beginning.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static int SequencePosition<T>(IEnumerable<T> sequence, IEnumerable<T> beginning)
		{
			// The sequence cannot be less than "beginning", so the loop doesn't get that far.
			for (int i = 0; i < sequence.Length () - beginning.Length (); i++) {

				//An amount of text equal to the length of the beginning sequence
				var chunk = Transformations.Subsequence (sequence, i, beginning.Length ());

				if (Transformations.Equal (chunk, beginning))
					return i;
			}

			// This sequence was not present in the list.
			return -1;
		}
	}
}

