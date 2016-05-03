using System;
using System.Collections.Generic;

using TR = HumDrum.Collections.Transformations;
using IF = HumDrum.Collections.Information;

using NUnit.Framework;

namespace HumDrumTests.Collections
{
	/// <summary>
	/// Test Fixture for HumDrum.Collections.Information.
	/// </summary>
	[TestFixture()]
	public class Information
	{
		/// <summary>
		/// List of numbers 0-10, used in testing
		/// </summary>
		private List<int> _testList;

		/// <summary>
		/// Initialize the Test List using Transformations
		/// </summary>
		[SetUp ()]
		public void Setup()
		{
			_testList = TR.MakeList (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
		}

		/// <summary>
		/// Tests Length
		/// </summary>
		[Test()]
		public void TestLength()
		{
			// Nothing
			Assert.AreEqual (
				IF.Length (new List<int> ()),
				0);

			// 11
			Assert.AreEqual (
				IF.Length (_testList),
				11);
		}

		/// <summary>
		/// Tests GetIndices
		/// </summary>
		[Test]
		public void TestGetIndices()
		{
			Assert.AreEqual (
				TR.Make (0, 1, 4, 4),
				IF.GetIndices (_testList, 0, 1, 4, 4));
		}

		/// <summary>
		/// Tests Get
		/// </summary>
		[Test()]
		public void TestGet()
		{
			// First
			Assert.AreEqual (
				IF.Get (_testList, 0),
				0);

			// Middle
			Assert.AreEqual (
				IF.Get (_testList, 5),
				5);
			
			// Last
			Assert.AreEqual (
				IF.Get (_testList, 10),
				10);
			
		}

		/// <summary>
		/// Tests Equal
		/// </summary>
		[Test()]
		public void TestEqual()
		{
			Assert.True (IF.Equal (_testList, TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10)));
		}

		/// <summary>
		/// Tests Times
		/// </summary>
		[Test()]
		public void TestTimes()
		{
 			// None
			Assert.AreEqual (
				IF.Times (_testList, 11),
				0);
			
			// One
			Assert.AreEqual (
				IF.Times (_testList, 4),
				1);

			// One on last element
			Assert.AreEqual (
				IF.Times (_testList, 10),
				1);
			
			// 3
			Assert.AreEqual (
				IF.Times (TR.Make (1, 1, 1, 2, 3), 1),
				3);
		}

		/// <summary>
		/// Tests Has
		/// </summary>
		[Test()]
		public void TestHas()
		{
			// No
			Assert.False(IF.Has(_testList, 11));

			// Yes - First
			Assert.True(IF.Has(_testList, 0));

			// Yes - Middle
			Assert.True(IF.Has(_testList, 5));

			// Yes - Last
			Assert.True(IF.Has(_testList, 10));

			// Middle - Sequence overload
			Assert.True (IF.Has (_testList, TR.Make (2, 3, 4)));

			// Beginning - Sequence overload
			Assert.True (IF.Has (_testList, TR.Make(0, 1, 2)));

			// End - Sequence Overload
			Assert.True (IF.Has (_testList, TR.Make (8, 9, 10)));

			Assert.False (IF.Has (_testList, TR.Make (0, 2, 4)));
		}

		/// <summary>
		/// Tests Genericize via Equality
		/// </summary>
		[Test()]
		public void TestGenericize()
		{
			Assert.AreEqual (
				IF.Genericize (TR.MakeList (1, 2, 3, 4, 5)),
				TR.Make (1, 2, 3, 4, 5));
		}

		/// <summary>
		/// Tests the positions function
		/// </summary>
		[Test]
		public void TestPositions()
		{
			Assert.AreEqual (
				IF.Positions (TR.Make (0, 1, 2, 3, 0, 4), 0),
				TR.Make (0, 4));
		}

		/// <summary>
		/// By virtue of both Head and Position passing the
		/// test, Position should as well. However I am still
		/// including a test for it
		/// </summary>
		[Test]
		public void TestPosition()
		{
			Assert.AreEqual (
				IF.Position (_testList, 4),
				4);
		}

		/// <summary>
		/// Tests the RelativeMembers function
		/// </summary>
		[Test]
		public void TestRelativeMembers()
		{
			Assert.AreEqual (
				TR.Make ('a', 'c', 'e'),
				IF.RelativeMembers (
					TR.Make (0, 2, 0, 4, 0, 6, 7, 8, 9, 10),
					0,
					TR.Make ('a', 'b', 'c', 'd', 'e', 'f', 'g', 'h')));
		}
	}
}

