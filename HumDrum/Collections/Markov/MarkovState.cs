using System;

namespace HumDrum.Collections.Markov
{
	public class MarkovState<T> 
	{
		public T[] State { get; set; }
		public T Next {get; set;}
		public double Probability {get; set;}

		public MarkovState(T[] state, T next)
		{
			State = state;
			Next = next;
			Probability = 0.00;
		}

		public MarkovState (T[] state, T next, double probability)
		{
			State = state;
			Next = next;
			Probability = probability;
		}
			
	}
}

