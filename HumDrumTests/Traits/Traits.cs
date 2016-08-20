using System;
using NUnit.Framework;
using TS = HumDrum.Traits;

namespace HumDrumTests.Traits
{
	/// <summary>
	/// Tests the Traits library
	/// </summary>
	[TestFixture]
	public class Traits
	{
		public interface ICanDoWork {
			int doWork (int workOn);
		}

		public class ICantDoWorkYet
		{

		}

		public class ICanWorkWork
		{
			public int doWork(int workOn)
			{
				return workOn + 1;
			}
		}

		/// <summary>
		/// Sets up this method
		/// </summary>
		[SetUp]
		public void Setup()
		{

		}

		public int doWork(int x) { return x + 1;}

		/// <summary>
		/// Tests whether or not traits work on the interface
		/// and class
		/// </summary>
		[Test]
		public void TestTraits()
		{
			

			// Test adding an existing method
			TS.Trait workTrait = new TS.Trait (new TS.Interface (typeof(ICanDoWork)), new TS.Class(typeof(ICantDoWorkYet)));
			workTrait.ImplementingClass.AddMethod (new TS.Method(typeof(ICanWorkWork).GetMethod("doWork")));

			Assert.True (workTrait.IsSatisfied ());

			// Test adding a local function
			TS.Trait workTrait2 = new TS.Trait(typeof(ICanDoWork), typeof(ICantDoWorkYet));

			workTrait2.AddMethod(this.GetType(), "doWork");
			

			Assert.True (workTrait2.IsSatisfied ());

			// Test adding an anonymous function with a name... kind of redundant, eh?
			TS.Trait workTrait3 = new TS.Trait(typeof(ICanDoWork), typeof(ICantDoWorkYet));
			workTrait3.AddMethod (new Func<int, int> (i => i + 1).Method, "doWork");

			Assert.True (workTrait3.IsSatisfied ());
		}
	}
}

