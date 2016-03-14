using System;
using System.Collections.Generic;

namespace HumDrum.Collections
{
	public static class Groups
	{		
		public delegate bool StateCheck<T>(T state);
		public delegate T StateModify<T>(T state);

		public abstract class StateObject<T>
		{
			public T State { get; set; }

			public StateCheck<T> Check { get; set;}
			public StateModify<T> Modifier { get; set; }
			
			public bool ModifyState(T capture)
			{
				State = Modifier (State);
				return Check (State);
			}

			public bool ModifyState()
			{
				State = Modifier (State);
				return Check (State);
			}

			public virtual void Reset()
			{
				State = default(T);
			}
		}

		public static List<List<T>> Group<T>(IEnumerable<T> list, StateObject<T> StateCheck)
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

			return Collection;
		}

		public static List<List<T>> Group<T,W>(IEnumerable<T> list, StateObject<W> StateCheck)
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

			return Collection;
		}
	}
}

