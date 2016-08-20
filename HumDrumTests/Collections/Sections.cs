using System;
using System.Collections.Generic;

using NUnit.Framework;

using TR = HumDrum.Collections.Transformations;
using SE = HumDrum.Collections.Sections;

namespace HumDrumTests.Collections
{
	/// <summary>
	/// Unit Tests for the Sections library. The Sections library
	/// is first implemented using generics, but provides an abstraction
	/// of these for text. For testing purposes, testing just the text functions
	/// provides adequate proof that the underlying functions are working as intended.
	/// </summary>
	[TestFixture]
	public class Sections
	{
		/// <summary>
		/// List of integers to eventually test
		/// </summary>
		private List<int> _testList;

		/// <summary>
		/// A string used to test Sections functions
		/// </summary>
		private string _testString;

		[SetUp]
		public void Setup()
		{
			_testList = TR.MakeList (1, 2, 3, 4, 5, 2, 3, 4, 5, 2, 5);
			_testString = "this{will test globs} {this{will{test}}internal} |this will {} test sections|";
		}

		/// <summary>
		/// This function tests ParseSections. 
		/// </summary>
		[Test]
		public void TestParseSections()
		{
			// Test it with a string
			Assert.AreEqual (
				TR.Make ("this{will", "test", "globs}", "{this{will{test}}internal}", "this will {} test sections"),
				SE.ParseSections (_testString, '|'));
		}

		/// <summary>
		/// Tests EscapeSplit
		/// </summary>
		[Test]
		public void TestEscapeSplit()
		{
			Assert.AreEqual (
				TR.Make ("this,is", " a sentence"),
				SE.EscapeSplit ("this\\,is, a sentence", ','));
		}

		/// <summary>
		/// Tests the Internal function
		/// </summary>
		[Test]
		public void TestInternal()
		{
			Assert.AreEqual (
				TR.Make ("will test globs", "this{will{test}}internal", ""),
				SE.Internal (_testString, '{', '}'));
		}

		/// <summary>
		/// Tests the globs function
		/// </summary>
		[Test]
		public void TestGlobs()
		{
			var actual = SE.Globs (_testString, '{', '}');

			Assert.AreEqual (
				TR.Make ("this{will test globs}", "{this{will{test}}internal}", "|this", "will", "{}", "test", "sections|"),
				actual);
		}

		/// <summary>
		/// Tests the "repair suite", which includes the functions:
		/// RepairSequenceWith, RepairSequence, RepairString, RepairStringWIth
		/// </summary>
		[Test]
		public void TestRepair()
		{
			/* Sequence */

			//RepairSequence
			Assert.AreEqual(TR.Make(1, 2, 3, 4, 5), SE.RepairSequence(TR.Make(TR.Make(1, 2, 3), TR.Make(4, 5))));

			// RepairSequenceWith
			Assert.AreEqual(TR.Make(1, 2, 3, 2, 5), SE.RepairSequenceWith(TR.Make(TR.Make(1), TR.Make(3), TR.Make(5)), 2));

			// RepairString
			Assert.AreEqual("the full string", SE.RepairString(TR.Make("the ", "full ", "string")));

			// RepairStringWith
			Assert.AreEqual("the full string", SE.RepairStringWith(TR.Make("the", "full", "string"), " ")); // Note: No spaces
		}
	}
}

