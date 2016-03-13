using System;
using System.Collections.Generic;

using HumDrum.Collections.Markov;

namespace HumDrum.Collections.Markov
{
	public class Markov<T>
	{
		public List<MarkovState<T>> States { get; private set; }

		public Markov (IEnumerable<T> dataset)
		{
			// 2 because it has to stop one before
			for(int i = 0; i < dataset.Length() - 2; i++) {
				double frequency = (double)dataset.Times (dataset.Get (i)) / (double)dataset.Length ();
				States.Add (
					new MarkovState<T> (
						dataset.Get (i), 
						dataset.Get (i + 1), 
						frequency));


			}
		}
	}
}

