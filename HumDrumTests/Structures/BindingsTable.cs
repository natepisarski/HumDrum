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

			_Table.Associate(ST.BindingsTable<string, int>.Bind (
				TR.Make ("a", "b", "c", "d"),
				TR.Make ( 0,    1,   2,   3)));

			// Test the resulting binding
			for (int i = 0; i < 4; i++)
				Assert.AreEqual (
					_Table.LookupFirst (IF.Get(TR.Make ("a", "b", "c", "d"), i)),
					IF.Get(TR.Make (0, 1, 2, 3), i));
		}

		/// <summary>
		/// Tests the static cross-product function as well as the Bind function
		/// by comparing them with each other.
		/// </summary>
		[Test]
		public void TestCrossAndBind()
		{
			var crossed = ST.BindingsTable<string, int>.Cross (TR.Make ("a", "b", "c"), TR.Make (1, 2, 3));

			var elements = ST.BindingsTable<string, int>.Bind (
				               TR.Make ("a", "a", "a", "b", "b", "b", "c", "c", "c"),
				               TR.Make (1, 2, 3, 1, 2, 3, 1, 2, 3));

			foreach (Tuple<string, int> member in elements)
				Assert.True (IF.Has (crossed, member));
		}

		/// <summary>
		/// Attempts to associate e with 4 in the Test List
		/// </summary>
		[Test]
		public void TestAssociate()
		{
			_Table.Associate ("e", 4);

			Assert.AreEqual (_Table.LookupFirst ("e"), 4);
		}

		/// <summary>
		/// Checks to see if the keyset is proper
		/// </summary>
		[Test]
		public void TestKeyset()
		{
			Assert.AreEqual (_Table.Keyset (), TR.Make ("a", "b", "c", "d"));
		}

		/// <summary>
		/// Checks to see if the values are proper
		/// </summary>
		[Test]
		public void TestValues()
		{
			Assert.AreEqual (_Table.Values (), TR.Make (0, 1, 2, 3));
		}

		public void Test
	}
}

