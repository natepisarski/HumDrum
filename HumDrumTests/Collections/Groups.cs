using System;
using System.Collections.Generic;

using TR = HumDrum.Collections.Transformations;
using GR = HumDrum.Collections.Groups;
using SM = HumDrum.Collections.StateModifiers;

using NUnit.Framework;

namespace HumDrumTests.Collections
{
	[TestFixture]
	public class Groups
	{
		private List<int> _testList;
		/// <summary>
		/// Initialize the Groups TestFixture
		/// </summary>
		[SetUp]
		public void Initialize()
		{
			_testList = TR.MakeList (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
		}


		/// <summary>
		/// Tests the two delegates (StateModify and StateCheck) from
		/// Groups.
		/// </summary>
		[Test]
		public void TestDelegates()
		{
			/* StateCheck Tests */
			GR.StateCheck<int> sc = x => x == 5;

			Assert.True (sc (5));
			Assert.False (sc (0));

			/* StateModify Tests */
			GR.StateModify<int> increaseByOne = x => x + 1;

			Assert.AreEqual (increaseByOne (5), 6);
			Assert.AreEqual (increaseByOne (-1), 0);
		}

		/// <summary>
		/// Tests Groups by using the only existing
		/// StateModifier. This also tests the StateModifier itself.
		/// </summary>
		[Test]
		public void TestGroups()
		{
			SM.IntegerCounter ic = new SM.IntegerCounter (5, 1);
			IEnumerable<IEnumerable<int>> expected = GR.Group<int> (_testList, ic);
			 
			Assert.AreEqual(
				expected,
				TR.Make(TR.Make(0, 1, 2, 3, 4, 5), TR.Make(6, 7, 8, 9, 10)));
		}
	}
}

