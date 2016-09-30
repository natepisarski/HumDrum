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
		/// Gets or sets the degree of this chian
		/// </summary>
		/// <value>The degree</value>
		public int Degree {get; private set;}

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

			AppendChain (dataset, degree);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Collections.Markov.Markov`1"/> class.
		/// This can initialize an instance of Markov without a dataset.
		/// </summary>
		/// <param name="degree">The degree of this chain</param>
		public Markov(int degree)
		{
			States = new List<MarkovState<T>> ();
			Degree = degree;
		}

		/// <summary>
		/// Appends the dataset to the current markov chain 
		/// </summary>
		/// <param name="dataset">The data to gather statistics from</param>
		/// <param name="degree">The degree of the markov chain</param>
		public void AppendChain(IEnumerable<T> dataset, int degree)
		{
			Degree = degree;
			// Pass 1: Determine the current state and the next element
			for (int i = 0; i < dataset.Length () - degree; i++) {
				var state = Transformations.Subsequence (dataset, i, degree).ToArray ();
				var future = dataset.Get (i + degree);
				States.Add(
					new MarkovState<T>(
						state,
						future));
			}

			// Pass 2: Determine the probability of current incurring future state
			foreach (MarkovState<T> ms in States) {
				List<T> occurences = (from MarkovState<T> item in States
					where Information.Equal(item.State, ms.State)
					select item.Next).ToList();
				// Probability is equal to the times this future state occured compared to how many there are.
				ms.Probability = ((double)Information.Times<T> (occurences, ms.Next)) / ((double)occurences.Length<T> ());
			}
		}

		/// <summary>
		/// Finds the probability that a particular seed incurs the given state
		/// </summary>
		/// <returns>The probability of the event</returns>
		/// <param name="state">The state to test for</param>
		public double ProbabilityOf(IEnumerable<T> seed, T next)
		{
			foreach (MarkovState<T> state in States) {
				if (state.State.SequenceEqual (seed))
					return state.Probability;
			}

			return 0.00;
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
				if (Information.Equal(currentState, state))
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

			// If something goes wrong, return a random state
			return States.Get(new Random().Next(0, States.Length()-1)).Next;
		}

		/// <summary>
		/// Creates a sequence of randomly selected instances
		/// based on a seed and a number of items to return.
		/// </summary>
		/// <returns>The random sequence</returns>
		/// <param name="seed">The seed state</param>
		/// <param name="count">How many iterations to go</param>
		public IEnumerable<T> SelectRandomSequence(IEnumerable<T> seed, int count)
		{
			IEnumerable<T> selectedState = seed;

			yield return SelectRandom (seed);
			for (; count > 0; count--) {
				selectedState = Transformations.Concatenate (
					Transformations.Subsequence (seed, 1, seed.Length ()),
					Transformations.Wrap (SelectRandom (selectedState)));
				yield return SelectRandom (selectedState);
			}
			yield break;
		}

		/// <summary>
		/// Starting with a random seed, select the given number of words.
		/// </summary>
		/// <returns>The random sequence</returns>
		/// <param name="count">The number of words to collect</param>
		public IEnumerable<T> SelectRandomSequence(int count) {
			return SelectRandomSequence(States.Get(new Random().Next(States.Length())).State, count);
		}

		/// <summary>
		/// Gets a random state from the list of available states
		/// </summary>
		/// <returns>The state</returns>
		public IEnumerable<T> RandomState()
		{
			return States.Get (new Random ().Next (0, States.Length () - 1)).State;
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="HumDrum.Collections.Markov.Markov`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="HumDrum.Collections.Markov.Markov`1"/>.</returns>
		public override string ToString()
		{
			string buffer = "";
			foreach (MarkovState<T> state in States) {
				foreach (T item in state.State)
					buffer += (item + " ");
				buffer += "| ";
				buffer += state.Next;
				buffer += "\n";
			}
			return buffer;
		}
	}
}

