using System;
using NUnit.Framework;

using HumDrum.Operations.Database;
using HumDrum.Collections;

namespace HumDrumTests
{
	/// <summary>
	/// Contains tests pertaining to the Database subsection of the Operations wing.
	/// </summary>
	[TestFixture]
	public class DatabaseTest
	{
		/// <summary>
		/// This tests the fluent interface to the Database subsection
		/// </summary>
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

		[Test]
		public static void TestRowOperations() {
			Table firstTable = TableBuilder.Start ()
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col1")
					.AddData (0)
					.AddData (1)
					.Finalize ())
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col2")
					.AddData (2)
					.AddData (3)
					.Finalize ())
				.Finalize ();

			Table secondTable = TableBuilder.Start ()
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col3")
					.AddData (0)
					.AddData (1)
					.Finalize ())
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col4")
					.AddData (2)
					.AddData (3)
					.Finalize ())
				.Finalize ();


			firstTable.AppendRow (new Row (secondTable, 1));

			Assert.AreEqual (3, firstTable.Rows.Length());
		}

		/// <summary>
		/// This will test the various functions that can be performed on a database. This,
		/// by extension, is an implicit test for essentially all functions for Row, RowItem, Schema, and SchemaAtom.
		/// </summary>
		[Test]
		public static void TestDatabaseOperations() {
			Table firstTable = TableBuilder.Start ()
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col1")
					.AddData (0)
					.AddData (1)
					.Finalize ())
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col2")
					.AddData (2)
					.AddData (3)
					.Finalize ())
				.Finalize ();

			Table secondTable = TableBuilder.Start ()
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col3")
					.AddData (4)
					.AddData (5)
					.Finalize ())
				.AddColumn (ColumnBuilder<int>.Start ()
					.Title ("col4")
					.AddData (6)
					.AddData (7)
					.Finalize ())
				.Finalize ();

			Table resultant = DatabaseOperations.CartesianProduct (firstTable, secondTable);

			var testResult = firstTable.GetSchema ();
			testResult.Add (secondTable.GetSchema ());

			Assert.AreEqual (resultant.GetSchema(), testResult);
		}

	}
}