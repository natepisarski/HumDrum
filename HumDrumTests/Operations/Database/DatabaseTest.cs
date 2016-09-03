using System;
using NUnit.Framework;

using HumDrum.Operations.Database;

namespace HumDrumTests
{
	[TestFixture]
	public class DatabaseTest
	{
		[Test]
		public static void TestDatabase()
		{
			// Creates a table
			Table t = TableBuilder.Start ()
				.AddColumn (
				          ColumnBuilder<int>.Start ()
					.Title ("firstRow")
					.AddData (0)
					.AddData (1)
					.AddData (2)
					.Finalize ())
				.AddColumn (
				          ColumnBuilder<int>.Start ()
					.Title ("secondRow")
					.AddData (3)
					.AddData (4)
					.AddData (5)
					.AddData (6)
					.Finalize ()).Finalize();

			Assert.AreEqual (0, t.GetColumn ("firstRow").ItemAt<int> (0));
		}
	}
}

