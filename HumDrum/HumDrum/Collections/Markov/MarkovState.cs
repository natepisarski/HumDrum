using System;
using System.Collections.Generic;

namespace HumDrum.Collections.Markov
{
	/// <summary>
	/// Contains information pertaining to the 
	/// state of one item within a markov chain.
	/// </summary>
	public class MarkovState<T> 
	{
		/// <summary>
		/// The state, of variable degree, which must
		/// be attained in order for Next to be considered.
		/// </summary>
		/// <value>The necessary state</value>
		public IEnumerable<T> State { get; set; }

		/// <summary>
		/// The item, that if State is reached, will be considered
		/// for return.
		/// </summary>
		/// <value>The future value</value>
		public T Next {get; set;}

		/// <summary>
		/// The probability that, given a state is reached, the 
		/// "Next" value will be returned
		/// </summary>
		/// <value>The probability to occur</value>
		public double Probability {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Collections.Markov.MarkovState`1"/> class.
		/// This will set the probability to 0.00 so that another process can set it
		/// </summary>
		/// <param name="state">State.</param>
		/// <param name="next">Next.</param>
		public MarkovState(IEnumerable<T> state, T next)
		{
			State = state;
			Next = next;
			Probability = 0.00;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Collections.Markov.MarkovState`1"/> class.
		/// If you already know the probability that something will occur, use this constructor instead
		/// </summary>
		/// <param name="state">The state of this instance</param>
		/// <param name="next">The future state of this instance</param>
		/// <param name="probability">The probability for this to occur</param>
		public MarkovState (IEnumerable<T> state, T next, double probability) : this(state, next)
		{
			Probability = probability;
		}
	}
}

