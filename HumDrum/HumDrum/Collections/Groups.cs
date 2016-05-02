
using System;
using System.Collections.Generic;

namespace HumDrum.Collections
{
	/// <summary>
	/// Groups use constructs similar to the idea
	/// of finite state machines to modify lists
	/// depending on the state of said machines.
	/// </summary>
	public static class Groups
	{		
		/// <summary>
		/// A function that checks the state
		/// and returns true if a certain condition is met.
		/// </summary>
		public delegate bool StateCheck<T>(T state);

		/// <summary>
		/// The function which modifies the state in a 
		/// pure way (i.e, returns the new value)
		/// </summary>
		public delegate T StateModify<T>(T state);

		/// <summary>
		/// The construct for the state machines. These functions
		/// set and apply the rules for checking and modifying state.
		/// </summary>
		public abstract class StateObject<T>
		{
			/// <summary>
			/// Gets or sets the current state of
			/// this machine.
			/// </summary>
			/// <value>The state</value>
			public T State { get; set; }

			/// <summary>
			/// The function used to check the
			/// state of this machine
			/// </summary>
			/// <value>The predicate function</value>
			public StateCheck<T> Check { get; set;}

			/// <summary>
			/// The function used to modify the state of this machine
			/// </summary>
			/// <value>The pure modifier function</value>
			public StateModify<T> Modifier { get; set; }

			/// <summary>
			/// Modifies the current state based on this instance's Modifier.
			/// This method is meant to be overriden to accomodate for the 
			/// captured element as an additional modification of the machine's state.
			/// </summary>
			/// <returns><c>true</c>, if the check AFTER modification is true<c>false</c> otherwise.</returns>
			/// <param name="capture">The item in a list</param>
			public virtual bool ModifyState(T capture)
			{
				State = Modifier (State);
				return Check (State);
			}

			/// <summary>
			/// Modify the state using only the modifier.
			/// </summary>
			/// <returns><c>true</c>, if state was modifyed, <c>false</c> otherwise.</returns>
			public bool ModifyState()
			{
				State = Modifier (State);
				return Check (State);
			}

			/// <summary>
			/// Function meant to be overriden by StateModifiers in order to reset after
			/// Groups parsing.
			/// </summary>
			public virtual void Reset()
			{
				State = default(T);
			}
		}

		/// <summary>
		/// Group this list based on the state of a StateObject.
		/// When the Check returns true, it will push a buffer to a list.
		/// This function returns all such buffers. Specifically, this function
		/// is meant to be used in StateModifiers which are based soley on their 
		/// internal state.
		/// </summary>
		/// <param name="list">The list to group</param>
		/// <param name="StateCheck">The StateObject to use in grouping</param>
		/// <typeparam name="T">A generic type parameter</typeparam>
		public static IEnumerable<IEnumerable<T>> Group<T>(IEnumerable<T> list, StateObject<T> StateCheck)
		{
			var Collection = new List<List<T>> ();
			var Buffer = new List<T> ();

			foreach (T item in list) {
				Buffer.Add (item);

				if(StateCheck.ModifyState (item))
				{
					Collection.Add (Buffer);
					StateCheck.Reset ();
					Buffer = new List<T> ();
				}
			}

			// Push any last minute data into the collection
			Collection.Add (Buffer);

			return Collection;
		}

		/// <summary>
		/// This Group method works similar to the other, but it relies on the state
		/// of the machine as well as elements from the list which are sent to the modier,
		/// similarly to a closure.
		/// </summary>
		/// <param name="list">The list to group</param>
		/// <param name="StateCheck">The StateObject to use</param>
		/// <typeparam name="T">The type of the list</typeparam>
		/// <typeparam name="W">The type the StateObject works with (almost always T)</typeparam>
		public static IEnumerable<IEnumerable<T>> Group<T,W>(IEnumerable<T> list, StateObject<W> StateCheck)
		{
			var Collection = new List<List<T>> ();
			var Buffer = new List<T> ();

			foreach (T item in list) {
				Buffer.Add (item);

				if(StateCheck.ModifyState ())
				{
					Collection.Add (Buffer);
					StateCheck.Reset ();
					Buffer = new List<T> ();
				}
			}

			Collection.Add (Buffer);
			return Collection;
		}
	}
}

