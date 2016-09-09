using System;
using System.Linq;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Collections
{
	public static class Transformations
	{

		/// <summary>
		/// Use varargs to create an array literal
		/// </summary>
		/// <param name="items">The items to add to the array</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static IEnumerable<T> Make<T>(params T[] items){
			foreach (T item in items)
				yield return item;
			yield break;
		}

		/// <summary>
		/// Creates a System.Generic.List object based on the
		/// items supplied to this method
		/// </summary>
		/// <returns>A list object containing all of these items</returns>
		/// <param name="items">The items to put into the list objects</param>
		/// <typeparam name="T">The type parameter</typeparam>
		public static List<T> MakeList<T>(params T[] items){
			return new List<T> (items);
		}

		/// <summary>
		/// Create a subsequence of the given collection. The length includes the first element
		/// </summary>
		/// <param name="list">The list to take the subsequence of</param>
		/// <param name="start">Where to start collecting</param>
		/// <param name="length">How many items to take</param>
		/// <typeparam name="T">T - type</typeparam>
		public static IEnumerable<T> Subsequence<T>(this IEnumerable<T> list, int start, int length){

			for (int i = start; length>0 && i < list.Length(); i++) {
				yield return (list.Get (i));
				length--;
			}

			yield break;
		}

		/// <summary>
		/// Returns a list where the first element of a given
		/// list is missing
		/// </summary>
		/// <param name="list">The list</param>
		/// <typeparam name="T">The type parameter</typeparam>
		public static IEnumerable<T> Tail<T>(this IEnumerable<T> list)
		{
			return Subsequence (list, 1, list.Length ());
		}

		/// <summary>
		/// Gets the first element of the list. Commonly used with Tail.
		/// </summary>
		/// <param name="list">The list</param>
		/// <typeparam name="T">The type of the list</typeparam>
		public static T Head<T>(this IEnumerable<T> list)
		{
			return list.Get (0);
		}

		/// <summary>
		/// Gets the last element of a specified list
		/// </summary>
		/// <param name="list">The list to find the last element of</param>
		/// <typeparam name="T">Generic type parameter</typeparam>
		public static T Last<T>(this IEnumerable<T> list)
		{
			return list.Get (list.Length () - 1);
		}

		/// <summary>
		/// Returns the list with the last element removed
		/// </summary>
		/// <returns>The list, with no last element</returns>
		/// <param name="list">The list to parse</param>
		/// <typeparam name="T">The type of information in this list</typeparam>
		public static IEnumerable<T> DropLast<T>(this IEnumerable<T> list)
		{
			return Transformations.RemoveAt (list, list.Length () - 1);
		}

		/// <summary>
		/// Remove an element at a specified index. This is meant to replace System.Generic.List.RemoveAt
		/// since this library works primarily with extending IEnumerable
		/// </summary>
		/// <returns>The list with an element removed<see cref="System.Collections.Generic.IEnumerable`1[[?]]"/>.</returns>
		/// <param name="list">The list to clean</param>
		/// <param name="index">The index to remove</param>
		public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> list, int index)
		{
			for (int i = 0; i < list.Length (); i++) {
				if (index == i)
					continue;

				yield return list.Get (i);
			}
			yield break;
		}
			
		/// <summary>
		/// Removes the duplicates from the list.
		/// </summary>
		/// <returns>The list, without any duplicates</returns>
		/// <param name="list">The list to scan</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static IEnumerable<T> RemoveDuplicates<T>(this IEnumerable<T> list)
		{
			var local = new List<T> ();

			foreach (T item in list) {
				if (!local.Contains (item))
					yield return item;
				local.Add (item);
			}

			yield break;
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
		public static IEnumerable<T> StartingWith<T>(IEnumerable<T> sequence, IEnumerable<T> beginning)
		{
			if (beginning.Length() == 1 && sequence.Last ().Equals (beginning.Get (0)))
				return Make(sequence.Last ());
			
			// The sequence cannot be less than "beginning", so the loop doesn't get that far.
			for (int i = 0; i < sequence.Length () - beginning.Length (); i++) {
				
				//An amount of text equal to the length of the beginning sequence
				var chunk = Transformations.Subsequence (sequence, i, beginning.Length ());

				if (Information.Equal (chunk, beginning))
					return Transformations.Subsequence (sequence, i, sequence.Length ());
			}

			// This sequence was not present in the list.
			return null;
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
			for (int i = 0; i < (sequence.Length () - beginning.Length () + 1); i++) {

				//An amount of text equal to the length of the beginning sequence
				var chunk = Transformations.Subsequence (sequence, i, beginning.Length ());

				if (Information.Equal (chunk, beginning))
					return i;
			}

			// This sequence was not present in the list.
			return -1;
		}
			
		/// <summary>
		/// Returns the given sequence as an array
		/// </summary>
		/// <returns>The list to convert</returns>
		/// <param name="list">An array based on the list</param>
		public static T[] AsArray<T>(this IEnumerable<T> list)
		{
			var collector = new List<T> ();
			collector.AddRange (list);
			return collector.ToArray ();
		}

		/// <summary>
		/// Create two arrays. The first array is made up of the 
		/// array's even elements (0, 2, ...) and the second is made up
		/// of the list's odd-indexed elements (1, 3, ...)
		/// </summary>
		/// <param name="originalList">The list to unbind from</param>
		/// <typeparam name="T">The type of this list.</typeparam>
		public static Tuple<IEnumerable<T>, IEnumerable<T>> Unbind<T>(IEnumerable<T> originalList){
			var firstList = new List<T> ();
			var secondList = new List<T> ();

			// If this is true, add the item to the first list. Second otherwise.
			bool firstOrSecond = true;

			foreach (T item in originalList) {
				if (firstOrSecond)
					firstList.Add (item);
				else
					secondList.Add (item);

				firstOrSecond = !firstOrSecond;
			}

			return new Tuple<IEnumerable<T>, IEnumerable<T>>
				(firstList.Genericize(), secondList.Genericize());
		}

		/// <summary>
		/// Zip the two lists together into a list of tuples. This will form points from the
		/// equivalent indices from the two lists
		/// </summary>
		/// <param name="firstList">The first list</param>
		/// <param name="secondList">The second list</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		/// <typeparam name="W">The 2nd type parameter.</typeparam>
		public static IEnumerable<Tuple<T, W>> Zip<T, W>(IEnumerable<T> firstList, IEnumerable<W> secondList)
		{
			for (int i = 0; i < firstList.Length () && i < secondList.Length (); i++)
				yield return new Tuple<T, W> (firstList.Get (i), secondList.Get (i));
			
			yield break;
		}

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
		/// "Tack" the item onto an existing IEnumerable list at the end.
		/// </summary>
		/// <param name="list">The list</param>
		/// <param name="item">The item</param>
		/// <typeparam name="T">The type of both parameters</typeparam>
		public static IEnumerable<T> Tack<T>(this IEnumerable<T> list, T item)
		{
			return Concatenate (list, Transformations.Wrap<T>(item));
		}
	}
}

