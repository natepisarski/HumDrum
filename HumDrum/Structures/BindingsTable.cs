using System;
using System.Linq;

using System.Collections.Generic;
using HumDrum.Collections;

namespace HumDrum.Structures
{
	public class BindingsTable<T, W>
	{
		public List<Tuple<T, W>> Bindings { get; private set; }

		public BindingsTable()
		{
			Bindings = new List<Tuple<T, W>> ();
		}

		public static List<Tuple<T, W>> Bind(IEnumerable<T> list1, IEnumerable<W> list2)
		{
			var localList = new List<Tuple<T, W>> ();

			for (int i = 0; i < list1.Length () - 1 && i < list2.Length() - 1; i++) 
				localList.Add (new Tuple<T, W> (list1.Get (i), list2.Get (i)));

			return localList;
		}

		public static List<Tuple<T, W>> Cross(IEnumerable<T> list1, IEnumerable<W> list2)
		{
			var local = new List<Tuple<T, W>> ();

			foreach (T item1 in list1) {
				foreach (W item2 in list2) {
					local.Add (new Tuple<T, W> (item1, item2));
				}
			}

			return local;
		}

		public void Associate(T key, W value)
		{
			Bindings.Add (new Tuple<T, W> (key, value));
		}

		public void Associate(T key, IEnumerable<W> value)
		{
			foreach (W item in value) 
				Bindings.Add (new Tuple<T, W> (key, item));
			
		}

		public void Associate(List<Tuple<T, W>> associations)
		{
			Bindings.AddRange (associations);
		}

		public T[] Keyset()
		{
			return Transformations.RemoveDuplicates((from x in Bindings
				select x.Item1).ToArray());
		}

		public W[] Values()
		{
			return Transformations.RemoveDuplicates((from x in Bindings
				select x.Item2).ToArray ());
		}

		public W[] Lookup(T key)
		{
			return (from x in Bindings
			        where x.Item1.Equals (key)
			        select x.Item2).ToArray ();
		}
	}
}

