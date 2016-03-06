﻿using System;
using System.Linq;

using System.Collections.Generic;
using HumDrum.Collections;

namespace HumDrum.Structures
{
	/// <summary>
	/// A class representing the binding between two
	/// types of data in a list.
	/// </summary>
	public class BindingsTable<T, W>
	{
		/// <summary>
		/// The bindings themselves.
		/// </summary>
		/// <value>The bindings.</value>
		public List<Tuple<T, W>> Bindings { get; private set; }

		/// <summary>
		/// Creates a BindingsTable with no associations.
		/// </summary>
		public BindingsTable()
		{
			Bindings = new List<Tuple<T, W>> ();
		}

		/// <summary>
		/// Bind the element in list1 to the corresponding element in list2.
		/// This will only associate as many elements as are in either list.
		/// </summary>
		/// <param name="list1">The first list (keyset)</param>
		/// <param name="list2">The second list (values)</param>
		public static List<Tuple<T, W>> Bind(IEnumerable<T> list1, IEnumerable<W> list2)
		{
			var localList = new List<Tuple<T, W>> ();

			for (int i = 0; i < list1.Length () - 1 && i < list2.Length() - 1; i++) 
				localList.Add (new Tuple<T, W> (list1.Get (i), list2.Get (i)));

			return localList;
		}

		/// <summary>
		/// Cross the specified list1 and list2. This will return a 
		/// list where every possible combination of 2 elements is returned.
		/// </summary>
		/// <param name="list1">The first list</param>
		/// <param name="list2">The second list.</param>
		public static List<Tuple<T, W>> Cross(IEnumerable<T> list1, IEnumerable<W> list2)
		{
			var local = new List<Tuple<T, W>> ();

			foreach (T item1 in list1) {
				foreach (W item2 in list2) 
					local.Add (new Tuple<T, W> (item1, item2));
			}

			return local;
		}

		/// <summary>
		/// Associate the specified key and value.
		/// </summary>
		/// <param name="key">The key</param>
		/// <param name="value">The value</param>
		public void Associate(T key, W value)
		{
			Bindings.Add (new Tuple<T, W> (key, value));
		}

		/// <summary>
		/// Associate the key with multiple values. This will
		/// put it into the structure as multiple pairs with the
		/// same key.
		/// </summary>
		/// <param name="key">The key to use</param>
		/// <param name="value">All of the values to use</param>
		public void Associate(T key, IEnumerable<W> value)
		{
			foreach (W item in value) 
				Bindings.Add (new Tuple<T, W> (key, item));
			
		}

		/// <summary>
		/// Add a pre-bound list of associations to the table
		/// </summary>
		/// <param name="associations">The associations to add to the list</param>
		public void Associate(List<Tuple<T, W>> associations)
		{
			Bindings.AddRange (associations);
		}

		/// <summary>
		/// Return the UNIQUE keyset from this BindingsTable. 
		/// </summary>
		public T[] Keyset()
		{
			return Transformations.RemoveDuplicates((from x in Bindings
				select x.Item1).ToArray());
		}

		/// <summary>
		/// Get just the UNIQUE values from this BindingsTable
		/// </summary>
		public W[] Values()
		{
			return Transformations.RemoveDuplicates((from x in Bindings
				select x.Item2).ToArray ());
		}

		/// <summary>
		/// Return all of the values associated with this key
		/// </summary>
		/// <param name="key">The key to look up in the bindings table</param>
		public W[] Lookup(T key)
		{
			return (from x in Bindings
			        where x.Item1.Equals (key)
			        select x.Item2).ToArray ();
		}
	}
}

