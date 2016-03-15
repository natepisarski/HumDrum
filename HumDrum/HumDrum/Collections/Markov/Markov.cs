using System;
using System.Linq;
using System.Collections.Generic;

using HumDrum.Collections;
using HumDrum.Collections.StateModifiers;
using HumDrum.Collections.Markov;

namespace HumDrum.Collections.Markov
{
	/// <summary>
	/// A representation of a markov chain for a certain
	/// type of data.
	/// </summary>
	public class Markov<T>
	{

		/// <summary>
		/// The list of states and their associated
		/// probabilities and results.
		/// </summary>
		/// <value>The states</value>
		public List<MarkovState<T>> States { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Collections.Markov.Markov`1"/> class.
		/// This will automatically parse dataset into States as MarkovStates. Obviously, with a large
		/// dataset this function (neighborhood ~ O(n^3)) will take a very long time.
		/// </summary>
		/// <param name="dataset">The set of data to analyze</param>
		/// <param name="degree">How many previous values define what a "state" is.</param>
		public Markov (IEnumerable<T> dataset, int degree)
		{
			States = new List<MarkovState<T>> ();
			var markovPairs = new List<T> ();

			// Pass 1: Determine the current state and the next element
			for (int i = 0; i < dataset.Length () - 1; i++) {
				var state = Transformations.Subsequence (dataset, i, degree).ToArray ();
				var future = dataset.Get (i + 1);
				States.Add(
					new MarkovState<T>(
						state,
						future));
			}

			// Pass 2: Determine the probability of current incurring future state
			foreach (MarkovState<T> ms in States) {
				List<T> occurences = (from MarkovState<T> item in States
					where Transformations.Equal(item.State, ms.State)
					select item.Next).ToList();
				// Probability is equal to the times this future state occured compared to how many there are.
				ms.Probability = ((double)Transformations.Times<T> (occurences, ms.Next)) / ((double)occurences.Length<T> ());
			}
		}

		/// <summary>
		/// Finds the possibilities given a certain state. Any MarkovState
		/// containing the state given will be returned.
		/// </summary>
		/// <param name="state">The list of states</param>
		public IEnumerable<MarkovState<T>> Possibilities(IEnumerable<T> state)
		{
			List<MarkovState<T>> collector = new List<MarkovState<T>> ();
		
			foreach (MarkovState<T> item in States) {
				var currentState = item.State;
				if (Transformations.Equal(currentState, state))
					collector.Add (item);
			}

			return collector;
		}

		/// <summary>
		/// Return a list of MarkovStates sorted by the most
		/// likely to occur to the least likely to occur.
		/// </summary>
		/// <returns>The likely states</returns>
		/// <param name="state">The original state to start at</param>
		public IEnumerable<MarkovState<T>> ByLikely(IEnumerable<T> state)
		{
			return Possibilities (state).OrderBy ((MarkovState<T> y) => y.Probability).Reverse().ToList();
		}

		/// <summary>
		/// The heart of the Markov principle. Based on the probability
		/// that each item will occur, return a random element 
		/// satisfying the given state.
		/// </summary>
		/// <returns>The random element</returns>
		/// <param name="state">The state to be attained</param>
		public T SelectRandom(IEnumerable<T> state)
		{
			// A random double in between 0 and 1
			var prob = new Random ().NextDouble ();

			var possible = ByLikely (state).ToList();

			foreach(MarkovState<T> likelyState in possible)
			{
				// More likely items are more likely to get prob below 0
				prob -= likelyState.Probability;
				if (prob <= 0.00)
					return likelyState.Next;
			}

			// If something goes wrong, return the most likely word to occur
			return ByLikely (state).Get (0).Next;
		}
	}
}

