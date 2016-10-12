using System;

using NUnit.Framework;

using HumDrum.Collections;
using HumDrum.Structures;

using DR = HumDrum.Structures.Direction;

namespace HumDrumTests.Structures
{
	/// <summary>
	/// Tests the Tree Structure
	/// </summary>
	[TestFixture]
	public class TreeTest
	{
		/// <summary>
		/// The tree used for testing
		/// </summary>
		private Tree<int> _testTree;

		/// <summary>
		/// Sets up the testTree.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			_testTree = new Tree<int> (0);

			for (int i = 0; i < 5; i++) {
				foreach (Tree<int> t in _testTree.Level(i)) {
					t.Grow (i + 1, DR.Left);
					t.Grow (i + 1, DR.Right);
				}
			}
		}

		/// <summary>
		/// This tests to see if the tree grew properly during
		/// the setup
		/// </summary>
		[Test]
		public void TestGrow()
		{

			for (int i = 1; i < 5; i++)
				Assert.AreEqual (Math.Pow(2, i), _testTree.Level (i).Length ());
			
		}

		/// <summary>
		/// Tests the parent functions
		/// </summary>
		[Test]
		public void TestParenthood()
		{
			// Check the RootParent method
			Assert.True(_testTree.Level(0).Get(0).IsRootParent());
			Assert.False (_testTree.Level (1).Get (0).IsRootParent ());

			// Check the IsParent method
			Assert.True(_testTree.Level(3).Get(0).IsParent());
			Assert.False (_testTree.Level (5).Get (0).IsParent ());

			// Check the IsIsoParent method
			Assert.True(_testTree.Level(3).Get(0).IsIsoParent());
		}

		/// <summary>
		/// Tests the GetValue function, which gets the 
		/// value of a certain direction
		/// </summary>
		[Test]
		public void TestGetValue()
		{
			// A code Haiku is
			// Random tree from level 3
			// Test the correctness

			Tree<int> tree = _testTree.Level (3).Get(0);

			// DOWN
			Assert.AreEqual(3, tree.GetValue(DR.Down));

			// UP
			Assert.AreEqual(2, tree.GetValue(DR.Up));

			// LEFT
			Assert.AreEqual(4, tree.GetValue(DR.Left));

			// RIGHT
			Assert.AreEqual(4, tree.GetValue(DR.Right));
		}

		/// <summary>
		/// Tests TotalMembers
		/// </summary>
		[Test]
		public void TestTotalMembers()
		{
			Assert.AreEqual (63, _testTree.TotalMembers());
				
		}

		/// <summary>
		/// Tests the flatten function
		/// </summary>
		[Test]
		public void TestFlatten()
		{
			Assert.AreEqual (
				Transformations.Make (3, 4, 5, 5, 4, 5, 5),
				_testTree.Level (3).Get (1).Flatten ());
		}

		/// <summary>
		/// Tests the Mapping function of the tree
		/// </summary>
		[Test]
		public void TestMap()
		{
			var myTree = new Tree<int> (0);
			myTree.Grow (1, 1);
			myTree.LeftBranch.Grow (2, 2);
			myTree.RightBranch.Grow (2, 2);

			myTree.Map (x => x + 2);

			Assert.AreEqual (2, myTree.CurrentNode);
			Assert.AreEqual (4, myTree.Level (2).Get (1).CurrentNode);
		}

		/// <summary>
		/// Tests the coalescing functions
		/// </summary>
		[Test]
		public void TestCoalesce()
		{
			var myTree = new Tree<int> (0);
			myTree.Grow (1, 1);
			myTree.LeftBranch.Grow (2, 2);
			myTree.RightBranch.Grow (2, 2);

			myTree.Snip (3);
			Func<int, int, int> coalescor = ((x, y) => x + y);

			myTree.Coalesce (coalescor, DR.Right);
			myTree.Coalesce (coalescor, DR.Right);

			Assert.AreEqual (8, myTree.CurrentNode);

			myTree = new Tree<int> (0);
			myTree.Grow (1, 1);
			myTree.LeftBranch.Grow (2, 2);
			myTree.RightBranch.Grow (2, 2);

			Func<int, int, int, int> coalescor2 = (x, y, z) => x + y + z;

			myTree.Coalesce (coalescor2, DR.Right);
			myTree.Coalesce (coalescor2, DR.Right);

			Assert.AreEqual (10, myTree.CurrentNode);
		}

		/// <summary>
		/// Tests the Prune function of the tree
		/// </summary>
		[Test]
		public void TestPrune()
		{
			var myTree = new Tree<int> (0);
			myTree.Grow (1, 1);
			myTree.LeftBranch.Grow (2, 2);
			myTree.RightBranch.Grow (2, 2);

			myTree.Prune (x => x > 1);

			Assert.AreEqual (
				3,
				myTree.TotalMembers());
		}

		/// <summary>
		/// Tests the OuterLeaves function of the tree
		/// </summary>
		[Test]
		public void TestOuterLeaves()
		{
			Assert.AreEqual (
				HigherOrder.Generate (5, 31, (x => x)),
				_testTree.OuterLeaves ());
		}
	}
}

