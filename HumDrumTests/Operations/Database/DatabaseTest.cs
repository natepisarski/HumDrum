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
		Table TestTableOne;
		Table TestTableTwo;

		[SetUp]
		public void InitializeInstance() 
		{
			// Sets up table one with the schema table1(firstColumn, secondColumn)
			TestTableOne = TableBuilder.Start ()
				.Title("table1")
				.AddColumn (
					ColumnBuilder<int>.Start ()
					.Title ("firstColumn")
					.AddData (0)
					.AddData (1)
					.AddData (2)
					.AddData(3)
					.Finalize ())
				.AddColumn (
					ColumnBuilder<int>.Start ()
					.Title ("secondColumn")
					.AddData (4)
					.AddData (5)
					.AddData (6)
					.AddData (7)
					.Finalize ()).Finalize();
			
			// Sets up table two with the schema table2(thirdColumn, fourthColumn
			TestTableTwo = TableBuilder.Start ()
				.Title("table2")
				.AddColumn (
					ColumnBuilder<int>.Start ()
					.Title ("thirdColumn")
					.AddData (0)
					.AddData (1)
					.AddData (2)
					.Finalize ())
				.AddColumn (
					ColumnBuilder<int>.Start ()
					.Title ("fourthColumn")
					.AddData (3)
					.AddData (4)
					.AddData (5)
					.AddData (6)
					.Finalize ()).Finalize();
		}
		/// <summary>
		/// This tests the fluent interface to the Database subsection
		/// </summary>
		[Test]
		public void TestDatabase()
		{
			// Simply tests to see if TableBuilder set up a proper table
			Assert.AreEqual (0, TestTableOne.GetColumn ("firstColumn").ItemAt<int> (0));
		}

		/// <summary>
		/// Test to see if manipualting rows on the table works
		/// </summary>
		[Test]
		public void TestRowOperations() {

			TestTableOne.AppendRow (new Row (TestTableTwo, 1));

			// The rows should be 
			Assert.AreEqual (4, TestTableOne.Rows.Length());
		}

		/// <summary>
		/// This will test the various functions that can be performed on a database. This,
		/// by extension, is an implicit test for essentially all functions for Row, RowItem, Schema, and SchemaAtom.
		/// </summary>
		[Test]
		public void TestCartesianProduct() {
			Table resultant = DatabaseOperations.CartesianProduct (TestTableOne, TestTableTwo);

			var testResult = TestTableOne.GetSchema ();
			testResult.Add (TestTableTwo.GetSchema ()); // Should be firstColumn, secondColumn, thirdColumn, fourthColumn

			Assert.AreEqual (resultant.GetSchema(), testResult);
		}

		/// <summary>
		/// Tests the implicit join function from the DatabaseOperations module
		/// </summary>
		[Test]
		public void TestImplicitJoin() 
		{
			var resultantSchema = TestTableOne.ImplicitJoin (TestTableTwo).GetSchema();

			var expectedSchema = 
				SchemaBuilder
					.Start ()
					.Add ("table1.firstColumn", typeof(int))
					.Add ("table1.secondColumn", typeof(int))
					.Add ("table2.thirdColumn", typeof(int))
					.Add ("table2.fourthColumn", typeof(int))
					.Finalize ();

			Assert.IsTrue (resultantSchema.Matches (expectedSchema));
		}
	}
}