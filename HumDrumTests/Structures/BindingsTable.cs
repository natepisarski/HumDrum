using System;
using NUnit.Framework;

using ST = HumDrum.Structures;
using TR = HumDrum.Collections.Transformations;
using IF = HumDrum.Collections.Information;
using CO = HumDrum.Constants;

namespace HumDrumTests.Structures
{
	/// <summary>
	/// Bindings table.
	/// </summary>
	[TestFixture]
	public class BindingsTable
	{
		private ST.BindingsTable<string, int> _Table;

		/// <summary>
		/// Sets up a BindingsTable for testing.
		/// This will bind (a, 0) (b, 1) ... (d, 3) using a static
		/// BindingsTable function and test its correctness
		/// </summary>
		[SetUp]
		[Test]
		public void Setup()
		{
			_Table = new ST.BindingsTable<string, int> ();

			_Table.Associate(TR.Bind (
				TR.Make ("a", "b", "c", "d"),
				TR.Make ( 0,    1,   2,   3)));

			// Test the resulting binding
			for (int i = 0; i < 4; i++)
				Assert.AreEqual (
					_Table.LookupFirst (IF.Get(TR.Make ("a", "b", "c", "d"), i)),
					IF.Get(TR.Make (0, 1, 2, 3), i));
		}

		/// <summary>
		/// Attempts to associate e with 4 in the Test List
		/// </summary>
		[Test]
		public void TestAssociate()
		{
			_Table.Associate ("e", 4);

			Assert.AreEqual (4, _Table.LookupFirst ("e"));
		}

		/// <summary>
		/// Checks to see if the keyset is proper
		/// </summary>
		[Test]
		public void TestKeyset()
		{
			Assert.AreEqual (TR.Make ("a", "b", "c", "d"), _Table.Keyset ());
		}

		/// <summary>
		/// Checks to see if the values are proper
		/// </summary>
		[Test]
		public void TestValues()
		{
			Assert.AreEqual (TR.Make (0, 1, 2, 3), _Table.Values ());
		}
	}
}

