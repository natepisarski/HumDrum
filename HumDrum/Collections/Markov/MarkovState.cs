using System;

namespace HumDrum.Collections.Markov
{
	public class MarkovState<T> 
	{
		public T State { get; set; }
		public Tuple<T, double> FutureState { get; set; }

		public MarkovState (T state, T next, double probability)
		{
			State = state;
			FutureState = new Tuple<T, double> (next, probability);
		}

		public double FutureProbability()
		{
			return FutureState.Item2;
		}

		public T FutureItem()
		{
			return FutureState.Item1;
		}
	}
}

