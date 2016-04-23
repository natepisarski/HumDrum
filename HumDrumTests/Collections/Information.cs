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
	}
}

