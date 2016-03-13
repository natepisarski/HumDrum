using System;
using HumDrum.Collections;

namespace HumDrum.Collections.StateModifiers
{
	public class IntegerCounter : Groups.StateObject<int>
	{
		public IntegerCounter (int cap, int increaseBy)
		{
			State = 0;

			Modifier = x => x + increaseBy;
			Check = x => x > cap;
		}

		public override void Reset()
		{
			State = 0;
		}
	}
}

