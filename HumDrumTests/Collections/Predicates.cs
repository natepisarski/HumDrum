﻿using System;
using System.Collections.Generic;

using NUnit.Framework;

using TR = HumDrum.Collections.Transformations;
using PR = HumDrum.Collections.Predicates;
using HO = HumDrum.Collections.HigherOrder;

namespace HumDrumTests.Collections
{
	/// <summary>
	/// Tests the Predicates library in HumDrum.Collections
	/// </summary>
	[TestFixture]	
	public class Predicates
	{
		/// <summary>
		/// The list used for testing
		/// </summary>
		private List<int> _testList;

		/// <summary>
		/// Sets up the variables used for testing.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			_testList = new List<int> ();
			_testList.AddRange (TR.Make (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
		}

		/// <summary>
		/// Tests both versions of the Any function
		/// </summary>
		[Test]
		public void TestAny()
		{
			/* Test the version for the IEnumerable<bool> */
			// True
			Assert.True (PR.Any (HO.ForEvery (_testList, x => x.Equals (0))));

			// False
			var truthList = HO.ForEvery(_testList, x => x.Equals(11));
			Assert.False (PR.Any (truthList));

			/* Test the version for IEnumerable<T> */

			// True
			Assert.True(PR.Any(_testList, x => x.Equals(10)));

			// False
			Assert.False(PR.Any(_testList, x => x.Equals(11)));
		}

		/// <summary>
		/// Tests both versions of the All function
		/// </summary>
		[Test]
		public void TestAll()
		{
			/* Tests the version for IEnumerable<bool> */

			// True
			Assert.True (PR.All (TR.Make (true, true, true, true, true)));
			
			// False
			Assert.False(PR.All(TR.Make(true, true, true, false)));

			/* Tests the version for IEnumerable<T> */

			// True
			Assert.True (
				PR.All (_testList, x => (x >= 0 && x <= 10)));

			// False
			Assert.False (
				PR.All (_testList, x => x > 5));

		}

		/// <summary>
		/// Tests DoTo and DoToSequence
		/// </summary>
		[Test]
		public void TestDoTo()
		{
			var actual = PR.DoTo (TR.Make (1, 2, 3, 4, 5), ((x) => x + 1 == 4), (x) => {
				return x + 2;
			});

			/* Tests DoTo */
			Assert.AreEqual (5, actual);

			/* Tests DoTo Sequence */
			Assert.AreEqual(
				TR.Make (3, 5),
				PR.DoToSequence (TR.Make (1, 2, 3, 4, 5), (x => (x % 2) == 0), (x => x + 1)));

		}

		/// <summary>
		/// Tests the first function
		/// </summary>
		[Test]
		public void TestFirst()
		{
			Assert.AreEqual(5, PR.First (TR.Make (1, 2, 3, 4, 5, 6), (x => x > 4)));
		}

		/// <summary>
		/// Tests the predicate generators. Currently the only
		/// generators in Predicates are for judging equality or membership.
		/// </summary>
		[Test]
		public void TestPredicateGenerators()
		{
			// Equality
			Assert.True(PR.GenerateEqualityPredicate(3)(3));

			Assert.False (PR.GenerateEqualityPredicate (3) (4));

			// Membership
			Assert.True (PR.GenerateEqualityPredicate (TR.Make (1, 2, 3, 4)) (2));

			Assert.False (PR.GenerateEqualityPredicate (TR.Make (1, 2, 3, 4)) (5));
		}

		/// <summary>
		/// Tests the reduce function
		/// </summary>
		[Test]
		public void TestReduce()
		{
			Assert.AreEqual (
				15, 
				PR.Reduce (
					TR.Make (1, 2, 3, 4, 5),
					(x, y) => (x + y),
					0));
		}

		/// <summary>
		/// This tests the two predicate constants,
		/// Tautology and Contradiction
		/// </summary>
		[Test]
		public void TestConstants()
		{
			// Tautology
			Assert.True(PR.All(_testList, PR.Tautology<int>()));

			// Contradiction
			Assert.False(PR.Any(_testList, PR.Contradiction<int>()));
		}
	}
}

