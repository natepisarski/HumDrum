using System;

using System.Collections.Generic;
using HumDrum.Collections;

namespace HumDrum.Structures
{
	/// <summary>
	/// A class representing the binding between two
	/// types of data. This works in a manor extremely similar to Hashmaps.
	/// </summary>
	[Stable]
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
		public void Associate(IEnumerable<Tuple<T, W>> associations)
		{
			Bindings.AddRange (associations);
		}

		/// <summary>
		/// Sets the value of the first item where the predicate is true to the value
		/// </summary>
		/// <param name="pred">The predicate to test for</param>
		/// <param name="value">The value to test for</param>
		public void SetAssociation(Predicate<T> pred, W value)
		{
			if (!Keyset().Any (pred))
				return;
			
			var toSet = Bindings.First (x => pred (x.Item1));
			toSet = new Tuple<T, W> (toSet.Item1, value);

			Bindings.Replace (x => x.Item1.Equals (toSet.Item1), toSet);
		}

		/// <summary>
		/// Set the existing association between an existing key and a new value.
		/// If there is no key with the name, it will not add it to the list of 
		/// associations
		/// </summary>
		/// <param name="key">The key to look for</param>
		/// <param name="value">The value to use for this key</param>
		public void SetAssociation(T key, W value)
		{
			SetAssociation (Predicates.GenerateEqualityPredicate (key), value);
		}

		/// <summary>
		/// Manually set the association. This has no benefits over SetAssociation, and it will be
		/// removed in future versions of HumDrum. However, at the time of writing this function,
		/// dynamic variables cannot be used in predicates, and will cause a runtime error when used this way.
		/// 
		/// So, if either T or W is dynamic, this is the only way to update an association.
		/// </summary>
		/// <param name="key">The key to look for</param>
		/// <param name="value">The value to associate with the key</param>
		public void SetAssociationManually(T key, W value)
		{
			List<Tuple<T, W>> newBindings = new List<Tuple<T, W>> ();

			foreach (Tuple<T, W> item in Bindings)
				if (key.Equals (item.Item1))
					newBindings.Add (new Tuple<T, W> (key, value));
				else
					newBindings.Add (item);

			Bindings = newBindings;
		}

		/// <summary>
		/// Return the UNIQUE keyset from this BindingsTable. 
		/// </summary>
		public IEnumerable<T> Keyset()
		{
			return Transformations.RemoveDuplicates (Bindings.ForEvery (x => x.Item1));
		}

		/// <summary>
		/// Get just the UNIQUE values from this BindingsTable
		/// </summary>
		public IEnumerable<W> Values()
		{
			return Transformations.RemoveDuplicates (Bindings.ForEvery (y => y.Item2));
		}

		/// <summary>
		/// Return all of the values associated with this key
		/// </summary>
		/// <param name="key">The key to look up in the bindings table</param>
		public IEnumerable<W> Lookup(T key)
		{
			return Lookup (Predicates.GenerateEqualityPredicate (key));
		}

		/// <summary>
		/// Looks up the information in this BindingsTable where the predicate holds true for the key
		/// </summary>
		/// <param name="predicate">The predicate to use</param>
		public IEnumerable<W> Lookup(Predicate<T> predicate)
		{
			return Bindings
				.When (x => predicate (x.Item1))
				.ForEvery (x => x.Item2);
		}

		/// <summary>
		/// Goes through this BindingsTable and finds the first occurence
		/// of the key, returning its value
		/// </summary>
		/// <returns>The first value associated with this key</returns>
		/// <param name="key">The key to look up</param>
		public W LookupFirst(T key)
		{
			return Lookup (key).Get (0);
		}

		/// <summary>
		/// Looks up the first item where the predicate yields true when applied to the key
		/// </summary>
		/// <returns>The first value where the key triggers the predicate</returns>
		/// <param name="predicate">The predicate to filter with</param>
		public W LookupFirst(Predicate<T> predicate)
		{
			return Lookup (predicate).Get (0);
		}

		/// <summary>
		/// Determines whether this instance has  key.
		/// </summary>
		/// <returns><c>true</c> if this instance has key; otherwise, <c>false</c>.</returns>
		/// <param name="key">The key to search for</param>
		public bool Has(T key)
		{
			return Keyset ().Has (key);
		}
			
	}
}

