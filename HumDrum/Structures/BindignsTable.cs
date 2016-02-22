using System;
using System.Collections.Generic;

namespace Todo
{

	public class BindingsTable<T, W>
	{

		public List<Tuple<T, W>> Bindings { get; private set; }


		public BindingsTable ()
		{
			Bindings = new List<Tuple<T, W>> ();
		}

		public void Associate(T key, W value)
		{
			Bindings.Add (new Tuple<T, W> (key, value));
		}

		public void Associate(T key, W[] values)
		{
			foreach (W item in values)
				Bindings.Add (new Tuple<T, W> (key, item));
		}

		public W[] Get(T key)
		{
			var local = new List<W> ();

			foreach (Tuple<T, W> item in Bindings)
				if (item.Item1.Equals (key))
					local.Add (item.Item2);
			
			return local.ToArray();
		}
	}
}

