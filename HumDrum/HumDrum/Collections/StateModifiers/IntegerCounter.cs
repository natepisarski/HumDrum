using System;
using HumDrum.Collections;

namespace HumDrum.Collections.StateModifiers
{
	/// <summary>
	/// A state machine used to count to a certain integer
	/// </summary>
	public class IntegerCounter : Groups.StateObject<int>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Collections.StateModifiers.IntegerCounter"/> class.
		/// "cap" is how high you wish to count to (from 0). increaseBy is how much each iteration should
		/// increase the State by.
		/// </summary>
		/// <param name="cap">The threshold value</param>
		/// <param name="increaseBy">The interative step</param>
		public IntegerCounter (int cap, int increaseBy)
		{
			State = 0;

			Modifier = x => x + increaseBy;
			Check = x => x > cap;
		}

		/// <summary>
		/// Reset this state
		/// </summary>
		public override void Reset()
		{
			State = 0;
		}
	}
}

