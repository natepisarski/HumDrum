using NUnit.Framework;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Collections
{
	[TestFixture ()]
	public class Test
	{
		[Test ()]
		public void TestSections()
		{			
			// ParseSections
			Assert.AreEqual (
				Sections.ParseSections ("|getThis| |andThis|", '|'),
				Transformations.Make ("getThis", "andThis"));

			// EscapeSplit
			Assert.AreEqual (
				Sections.EscapeSplit ("some\\ te xt", ' '),
				Transformations.Make ("some te", "xt"));

			// Internal
			Assert.AreEqual (
				Sections.Internal ("no{yes{yes}yes}no", '{', '}'),
				"yes{yes}yes");

			// RepairString
			Assert.AreEqual (
				Sections.RepairString (Transformations.Make ("1", "2", "3")),
				"1 2 3");
		}

		[Test ()]
		public void TestTransformations(){

			int[] testArray = { 1, 2, 3 };

			// Length
			Assert.AreEqual (
				Transformations.Length (Transformations.Make (1, 2, 3)),
				3);


			// Get
			Assert.AreEqual (
				Transformations.Get (Transformations.Make (1, 2, 3, 4, 5), 2),
				3);
		
			// Make
			Assert.AreEqual (
				Transformations.Make (1, 2, 3),
				testArray);

			// MakeList
			Assert.AreEqual (
				Transformations.MakeList (1, 2, 3),
				new List<int> (Transformations.Make (1, 2, 3)));

			// Subsequence
			Assert.AreEqual (
				Transformations.Subsequence (Transformations.Make (1, 2, 3, 4, 5), 1, 5),
				Transformations.Make (2, 3, 4, 5));
		}
	}
}

