using System;
using System.Linq;
using System.Collections.Generic;

using HumDrum.Collections;
using HumDrum.Collections.StateModifiers;
using HumDrum.Collections.Markov;

namespace HumDrum.Collections.Markov
{
	public class Markov<T>
	{
		public List<MarkovState<T>> States { get; private set; }

		public Markov (IEnumerable<T> dataset, int degree)
		{
			States = new List<MarkovState<T>> ();
			var markovPairs = new List<T> ();

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
				
				ms.Probability = ((double)Transformations.Times<T> (occurences, ms.Next)) / ((double)occurences.Length<T> ());
			}
		}

		//TODO: This can be optimized heavily
		public IEnumerable<MarkovState<T>> Possibilities(IEnumerable<T> state)
		{
			List<MarkovState<T>> collector = new List<MarkovState<T>> ();
			//TODO: This can be made into a LINQ query
			foreach (MarkovState<T> item in States) {
				var currentState = item.State;
				if (Transformations.Equal(currentState, state))
					collector.Add (item);
			}
			return collector;
		}

		public IEnumerable<MarkovState<T>> ByLikely(IEnumerable<T> state)
		{
			List<MarkovState<T>> DEBUG_VARIABLE = Possibilities (state).OrderBy ((MarkovState<T> y) => y.Probability).Reverse().ToList();
			return DEBUG_VARIABLE; //TODO: Probably don't need this either
		}

		public T SelectRandom(IEnumerable<T> state)
		{
			var prob = new Random ().NextDouble ();
			var possible = ByLikely (state).ToList();

			foreach(MarkovState<T> likelyState in possible)
			{
				prob -= likelyState.Probability;
				if (prob <= 0.00)
					return likelyState.Next;
			}
			return ByLikely (state).Get (0).Next;
		}
	}
}

