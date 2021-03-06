﻿using System;
using System.Collections.Generic;

using NUnit.Framework;

using HO = HumDrum.Collections.HigherOrder;
using TR = HumDrum.Collections.Transformations;
using PR = HumDrum.Collections.Predicates;

namespace HumDrumTests.Collections
{
	[TestFixture]
	public class HigherOrder
	{
		private List<int> _testList;

		[SetUp]
		public void Setup ()
		{
			_testList = new List<int> ();
			_testList.AddRange (TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
		}

		/// <summary>
		/// Despite Transformer being a delegate,
		/// this unit test attempts to cast a lambda
		/// to a transformer and use it.
		/// </summary>
		public void TestTransformer()
		{
			HO.Transformer<string, int> numOfChars = ((string x) => x.Length);

			Assert.AreEqual (4, numOfChars ("abcd"));
			Assert.AreEqual (2, numOfChars ("ab"));
		}

		/// <summary>
		/// Tests the when function
		/// </summary>
		[Test]
		public void TestWhen()
		{
			// Only even numbers
			Assert.AreEqual (
				TR.Make (0, 2, 4, 6, 8, 10),
				HO.When (_testList, (x => x % 2 == 0)));

			// Natural Numbers
			Assert.AreEqual (
				TR.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
				HO.When (_testList, x => x > 0));
		}

		/// <summary>
		/// Tests the ForEvery function
		/// </summary>
		[Test]
		public void TestForEvery()
		{
			// Negative Equivalent
			Assert.AreEqual (
				TR.Make (0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10),
				HO.ForEvery (_testList, x => x * (-1)));

			// Double
			Assert.AreEqual (
				TR.Make (0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20),
				HO.ForEvery (_testList, x => x * 2));
		}

		/// <summary>
		/// Tests the DoAll function
		/// </summary>
		[Test]
		public void TestDoAll()
		{
			// Are they all less than 10?
			Assert.False(HO.DoAll(_testList, x => x < 10));
				
			// Are they all less than or equal to 10?
			Assert.True(HO.DoAll(_testList, x => x <= 10));
		}

		/// <summary>
		/// Tests the Collapse function
		/// </summary>
		[Test]
		public void TestCollapse()
		{
			Assert.AreEqual (
				10,
				HO.Collapse (TR.Make (1, 2, 3, 4), ((x, y) => x + y)));
		}

		/// <summary>
		/// Tests the inclusive version of While
		/// </summary>
		[Test]
		public void TestWhileInclusive()
		{
			// Every number less than or equal to 5
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4, 5),
				HO.WhileInclusive (_testList, x => x < 5));

			// Square is less than 10
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4),
				HO.WhileInclusive (_testList, x => (x * x) < 10));
		}

		/// <summary>
		/// Tests the inclusive version of after
		/// </summary>
		[Test]
		public void TestAfterInclusive()
		{
			// Every number 5 or greater
			Assert.AreEqual (
				TR.Make (5, 6, 7, 8, 9, 10),
				HO.AfterInclusive (_testList, x => (x == 5)));

			// Just 10
			Assert.AreEqual (
				TR.Make (10),
				HO.AfterInclusive (_testList, x => x == 10));
		}

		/// <summary>
		/// Tests the replace function
		/// </summary>
		[Test]
		public void TestReplace()
		{
			var x = HO.Replace (TR.Make (1, 2, 3, 4, 5, 6), PR.GenerateEqualityPredicate (1), 2);

			Assert.AreEqual (
				TR.Make (2, 2, 3, 4, 5, 6),
				x);
		}

		/// <summary>
		/// Tests the non-inclusive variant of the while function
		/// </summary>
		[Test]
		public void TestWhile()
		{
			// Numbers 4 and lesser
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4),
				HO.While (_testList, (x => x != 5)));

			// Nothing
			Assert.AreEqual (
				new List<int> (),
				HO.While (_testList, x => x > 10));
		}

		/// <summary>
		/// Tests the non-inclusive variant of the after function
		/// </summary>
		[Test]
		public void TestAfter()
		{
			// 6-10
			Assert.AreEqual (
				TR.Make (6, 7, 8, 9, 10),
				HO.After (_testList, x => x == 5));

			// Nothing
			Assert.AreEqual (
				new List<int> (),
				HO.After (_testList, x => x == 11));
		}

		/// <summary>
		/// Tests the generate function
		/// </summary>
		[Test]
		public void TestGenerate()
		{
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3, 4, 5),
				HO.Generate (0, 5, (x => x + 1)));
		}

		/// <summary>
		/// Tests both the before and beforeInclusive
		/// </summary>
		[Test]
		public void TestBefore()
		{
			var testList = TR.Make (0, 1, 2, 3, 4, 5);

			var expectedBefore = HO.Before (testList, (PR.GenerateEqualityPredicate (3)));
			// Before
			Assert.AreEqual (
				expectedBefore,
				TR.Make (0, 1, 2));

			// BeforeInclusive
			Assert.AreEqual (
				TR.Make (0, 1, 2, 3), 
				HO.BeforeInclusive (testList, PR.GenerateEqualityPredicate (3)));
		}

		/// <summary>
		/// Tests the after and afterInclusive functions
		/// </summary>
		[Test]
		public void TestAfterHigherOrder()
		{
			var testList = TR.Make (0, 1, 2, 3, 4, 5);

			// After
			Assert.AreEqual(TR.Make(2, 3, 4, 5), HO.After(testList, PR.GenerateEqualityPredicate(1)));

			// AfterInclusive
			Assert.AreEqual(TR.Make(1, 2, 3, 4, 5), HO.AfterInclusive(testList, PR.GenerateEqualityPredicate(1)));
		}
	}
}