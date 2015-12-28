using NUnit.Framework;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Recursion
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestTailHelper ()
		{
			// Wrap
			Assert.AreEqual (
				TailHelper.Wrap (1),
				Transformations.MakeList (1));

			// Concatenate
			Assert.AreEqual (
				TailHelper.Concatenate (
					Transformations.MakeList (1, 2, 3),
					Transformations.MakeList (4, 5, 6)),
				Transformations.MakeList (1, 2, 3, 4, 5, 6));
		}
	}
}

