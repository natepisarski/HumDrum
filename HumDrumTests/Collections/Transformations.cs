using System;
using System.Collections.Generic;

using NUnit.Framework;

using TR = HumDrum.Collections.Transformations;
using IF = HumDrum.Collections.Information;

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
		[Test]
		public void TestSubsequence()
		{
			// Full
			Assert.AreEqual (
				_testList,
				TR.Subsequence (_testList, 0, 11));
		
			// First 5
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4),
				TR.Subsequence (_testList, 0, 5));
			
			// 5-8
			Assert.AreEqual (
				TR.Make (5, 6, 7, 8),
				TR.Subsequence (_testList, 5, 4));
		}

		/// <summary>
		/// Tests Tail
		/// </summary>
		[Test()]
		public void TestTail()
		{
			Assert.AreEqual (
				TR.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
				TR.Tail (_testList));

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
				1,
				TR.Head (TR.Make (1, 2, 3)));
		}

		/// <summary>
		/// Tests the Last function
		/// </summary>
		[Test]
		public void TestLast()
		{
			// Test on longish list
			Assert.AreEqual (
				5,
				TR.Last (TR.Make (1, 2, 3, 4, 5)));

			// Test on singleton list
			Assert.AreEqual (
				1,
				TR.Last (TR.Make (1)));
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
				TR.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
				TR.RemoveAt (_testList, 0));

			// Inner
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4, 6, 7, 8, 9, 10),
				TR.RemoveAt (_testList, 5));

			// Last
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9),
				TR.RemoveAt (_testList, 10));
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
				_testList,
				TR.RemoveDuplicates (secondTestList));
		}

		/// <summary>
		/// Tests StartingWith
		/// </summary>
		[Test()]
		public void TestStartingWith()
		{
			// First
			Assert.AreEqual (
				_testList,
				TR.StartingWith (_testList, TR.Make (0, 1)));

			// Middle
			Assert.AreEqual (
				TR.Make (5, 6, 7, 8, 9, 10),
				TR.StartingWith (_testList, TR.Make(5)));
			
			// Last
			Assert.AreEqual (
				TR.Make (10),
				TR.StartingWith (_testList, TR.Make(10)));
			
		}

		/// <summary>
		/// Tests SequencePosition
		/// </summary>
		[Test()]
		public void TestSequencePosition()
		{
			// First
			Assert.AreEqual (
				0,
				TR.SequencePosition (_testList, TR.Make (0, 1, 2)));

			// 5
			Assert.AreEqual (
				5,
				TR.SequencePosition (_testList, TR.Make (5, 6, 7)));

			// 10
			Assert.AreEqual (
				10,
				TR.SequencePosition (_testList, TR.Make (10)));
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
		/// Tests the AsList function
		/// </summary>
		public void TestAsList()
		{
			Assert.AreEqual (
				_testList,
				TR.AsList (_testList));
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
				TR.Make (0, 2, 4, 6, 8, 10),
				unbound.Item1);
			
			// Odd
			Assert.AreEqual (
				TR.Make(1, 3, 5, 7, 9),
				unbound.Item2);
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

			Assert.AreEqual (TR.Wrap (1), x);
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

		/// <summary>
		/// Tests the right shift function
		/// </summary>
		[Test]
		public void TestRightShift()
		{
			// Tests the right shift with positions=1
			Assert.AreEqual (
				TR.Make (6, 1, 2, 3, 4, 5),
				TR.RightShift (TR.Make (1, 2, 3, 4, 5, 6)));

			// Tests the right shift with positions=4
			Assert.AreEqual (
				TR.Make (2, 3, 4, 5, 1),
				TR.RightShift (TR.Make (1, 2, 3, 4, 5), 4));
		}

		/// <summary>
		/// Tests the left shift function
		/// </summary>
		[Test]
		public void TestLeftShift()
		{
			// Tests the left shift with positions=1
			Assert.AreEqual (
				TR.Make (2, 3, 4, 1),
				TR.LeftShift (TR.Make (1, 2, 3, 4)));

			// Tests the left shift with positions=3
			Assert.AreEqual (
				TR.Make (4, 1, 2, 3),
				TR.LeftShift (TR.Make (1, 2, 3, 4), 3));
		}

		/// <summary>
		/// Tests the Prepend function
		/// </summary>
		[Test]
		public void TestPrepend()
		{
			// Test with an item
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4),
				TR.Prepend (TR.Make (1, 2, 3, 4), 0));

			// Test with a list
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4),
				TR.Prepend (TR.Make (3, 4), TR.Make (0, 1, 2)));
		}

		/// <summary>
		/// Tests the reverse function
		/// </summary>
		[Test]
		public void TestReverse() {
			Assert.AreEqual (
				TR.Make (5, 4, 3, 2, 1),
				TR.Reverse (TR.Make (1, 2, 3, 4, 5)));
		}

		/// <summary>
		/// Tests the static cross-product function as well as the Bind function
		/// by comparing them with each other.
		/// </summary>
		[Test]
		public void TestCrossAndBind()
		{
			var crossed = TR.Cross (TR.Make ("a", "b", "c"), TR.Make (1, 2, 3));

			var elements = TR.Bind (
				TR.Make ("a", "a", "a", "b", "b", "b", "c", "c", "c"),
				TR.Make (1, 2, 3, 1, 2, 3, 1, 2, 3));

			foreach (Tuple<string, int> member in elements)
				Assert.True (IF.Has (crossed, member));
		}
	}
}

