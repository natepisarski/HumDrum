using System;
using System.Collections.Generic;

using NUnit.Framework;

using TR = HumDrum.Collections.Transformations;

namespace HumDrumTests.Collections
{
	/// <summary>
	/// Test Fixture for HumDrum's Transformations library. 
	/// This attempts to find points of failure by testing odd cases.
	/// </summary>
	[TestFixture ()]
	public class Transformations
	{
		private List<int> _testList;

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrumTests.Collections.Transformations"/> class.
		/// This will set up the test list.
		/// </summary>
		[SetUp ()]
		public void Initialize ()
		{
			_testList = new List<int> ();

			// Populate the list with 0-10
			for (int i = 0; i < 11; i++)
				_testList.Add (i);
			
		}

		/// <summary>
		/// Tests HumDrum.Collections.TransformationsMake. From here on out,
		/// much of the unit tests are tested using Make.
		/// </summary>
		[Test ()]
		public void TestMake()
		{
			IEnumerable<int> zeroToTen = TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
			int counter = 0;

			foreach (int item in zeroToTen) {
				Assert.AreEqual (_testList [counter], item);
				counter++;
			}
		}



		/// <summary>
		/// Tests Subsequence
		/// </summary>
		[Test()]
		public void TestSubsequence()
		{
			// Full
			Assert.AreEqual (
				TR.Subsequence (_testList, 0, 11),
				_testList);
			
			// First 5
			Assert.AreEqual (
				TR.Subsequence (_testList, 0, 5),
				TR.Make (0, 1, 2, 3, 4));
			
			// 5-8
			Assert.AreEqual (
				TR.Subsequence (_testList, 5, 4),
				TR.Make (5, 6, 7, 8));
		}

		/// <summary>
		/// Tests Tail
		/// </summary>
		[Test()]
		public void TestTail()
		{
			Assert.AreEqual (
				TR.Tail (_testList),
				TR.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));

			Assert.AreEqual (
				new List<int> (),
				TR.Tail (TR.Make (1)));
		}

		/// <summary>
		/// Tests Head
		/// </summary>
		[Test]
		public void TestHead()
		{
			// It should simply return the first element
			Assert.AreEqual (
				TR.Head (TR.Make (1, 2, 3)),
				1);
		}

		/// <summary>
		/// Tests the Last function
		/// </summary>
		[Test]
		public void TestLast()
		{
			// Test on longish list
			Assert.AreEqual (
				TR.Last (TR.Make (1, 2, 3, 4, 5)), 5);

			// Test on singleton list
			Assert.AreEqual (
				TR.Last (TR.Make (1)), 1);
		}

		/// <summary>
		/// Tests the DropLast function
		/// </summary>
		[Test]
		public void TestDropLast()
		{
			Assert.AreEqual (TR.Make (1, 2, 3, 4, 5), TR.DropLast (TR.Make (1, 2, 3, 4, 5, 6)));
		}

		/// <summary>
		/// Tests HumDrum.Collections.Transformations.RemoveAt
		/// </summary>
		[Test()]
		public void TestRemoveAt()
		{
			// First
			Assert.AreEqual (
				TR.RemoveAt (_testList, 0),
				TR.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10));

			// Inner
			Assert.AreEqual (
				TR.RemoveAt (_testList, 5),
				TR.Make (0, 1, 2, 3, 4, 6, 7, 8, 9, 10));

			// Last
			Assert.AreEqual (
				TR.RemoveAt (_testList, 10),
				TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9));
		}

		/// <summary>
		/// Tests RemoveDuplicates
		/// </summary>
		[Test()]
		public void TestRemoveDuplicates()
		{
			List<int> secondTestList = new List<int> ();

			for (int i = 0; i < 11; i++)
				for (int b = 0; b < 2; b++)
					secondTestList.Add (i);

			Assert.AreEqual (
				TR.RemoveDuplicates (secondTestList),
				_testList);
		}

		/// <summary>
		/// Tests StartingWith
		/// </summary>
		[Test()]
		public void TestStartingWith()
		{
			// First
			Assert.AreEqual (
				TR.StartingWith (_testList, TR.Make (0, 1)),
				_testList);

			// Middle
			Assert.AreEqual (
				TR.StartingWith (_testList, TR.Make(5)),
				TR.Make (5, 6, 7, 8, 9, 10));
			
			// Last
			Assert.AreEqual (
				TR.StartingWith (_testList, TR.Make(10)),
				TR.Make (10));
			
		}

		/// <summary>
		/// Tests SequencePosition
		/// </summary>
		[Test()]
		public void TestSequencePosition()
		{
			// First
			Assert.AreEqual (
				TR.SequencePosition (_testList, TR.Make (0, 1, 2)),
				0);

			// 5
			Assert.AreEqual (
				TR.SequencePosition (_testList, TR.Make (5, 6, 7)),
				5);

			// 10
			Assert.AreEqual (
				TR.SequencePosition (_testList, TR.Make (10)),
				10);
		}

		/// <summary>
		/// Tests AsArray
		/// </summary>
		[Test()]
		public void TestAsArray()
		{
			Assert.AreEqual (
				_testList.ToArray (),
				TR.AsArray (_testList));
		}

		/// <summary>
		/// Tests Unbind
		/// </summary>
		[Test()]
		public void TestUnbind()
		{
			var unbound = TR.Unbind (_testList);

			// Even
			Assert.AreEqual (
				unbound.Item1, 
				TR.Make (0, 2, 4, 6, 8, 10));
			
			// Odd
			Assert.AreEqual (
				unbound.Item2, 
				TR.Make(1, 3, 5, 7, 9));
		}

		/// <summary>
		/// Tests the zip function
		/// </summary>
		[Test]
		public void TestZip()
		{
			// 0 1 2
			// 1 2 3
			// Tupples will go vertically

			Assert.AreEqual (
				TR.Make (
					new Tuple<int, int> (0, 1),
					new Tuple<int, int> (1, 2),
					new Tuple<int, int> (2, 3)),
				TR.Zip (TR.Make (0, 1, 2), TR.Make (1, 2, 3)));
		}

		/// <summary>
		/// Tests Wrap
		/// </summary>
		[Test]
		public void TestWrap()
		{
			var x = new List<int> ();
			x.Add (1);

			Assert.AreEqual (x, TR.Wrap (1));
		}

		/// <summary>
		/// Tests Concatenate
		/// </summary>
		[Test]
		public void TestConcatenate()
		{
			Assert.AreEqual (
				_testList, 
				TR.Concatenate (TR.Make (0, 1, 2, 3, 4, 5), TR.Make (6, 7, 8, 9, 10)));
		}

		/// <summary>
		/// Tests Tack
		/// </summary>
		[Test]
		public void TestTack()
		{
			Assert.AreEqual (
				_testList,
				TR.Tack (TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9), 10));
		}
	}
}

