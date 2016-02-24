using System;
using System.Collections.Generic;

using HumDrum.Collections;

namespace Todo
{
	public static class Predicates
	{
		public static bool Any(List<bool> list)
		{
			if (list.Length() == 0)
				return true;
			else
				return list.Get<bool> (0) || Any (list.Tail ());
		}

		public static bool All(List<bool> list)
		{
			if (list.Length() == 0)
				return true;
			else
				return list.Get<bool> (0) && Any (list.Tail<bool> ());
		}
	}
}

