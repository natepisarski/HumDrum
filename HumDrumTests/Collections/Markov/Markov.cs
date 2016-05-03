using System;
using System.Collections.Generic;

using NUnit.Framework;

using MK = HumDrum.Collections.Markov;
using TR = HumDrum.Collections.Transformations;
using GR = HumDrum.Collections.Groups;
using SO = HumDrum.Collections.StateModifiers;
using IF = HumDrum.Collections.Information;
using HO = HumDrum.Collections.HigherOrder;

namespace HumDrumTests.Collections.Markov
{
	/// <summary>
	/// This class contains the unit tests for the entirety of 
	/// the Markov namespace. This is because MarkovState is used 
	/// as a struct, and in future versions of HumDrum may actually be 
	/// implemented as such.
	/// </summary>
	[TestFixture]
	public class Markov
	{
		/// <summary>
		/// The list used for testing.
		/// </summary>
		private List<int> _testList;

		[SetUp]
		public void Setup()
		{
			_testList = new List<int> ();

			// Indices:                  0  1  2  3  4  5  6  7  8  9 10  11 12 13
			_testList.AddRange (TR.Make (0, 1, 2, 3, 2, 4, 5, 6, 0, 1, 7, 8, 9, 10));
		}


		/// <summary>
		/// Tests the initialization of a Markov chain given
		/// a certain degree
		/// </summary>
		/// <param name="degree">The degree to test</param>
		public void TestInitialization(int degree)
		{
			MK.Markov<int> markovChain = new MK.Markov<int> (_testList, degree);

			Assert.AreEqual (_testList.Count - degree, markovChain.States.Count);
		}

		/// <summary>
		/// Tests the initialization of a few degrees
		/// </summary>
		[Test]
		public void TestInitialization()
		{
			TestInitialization (1);
			TestInitialization (2);
		}

		/// <summary>
		/// Tests the probabilities that some things will occur in the 
		/// Markov Chain.
		/// </summary>
		[Test]
		public void TestProbabilities()
		{
			// Tests the chain of degree 1
			MK.Markov<int> markovChain1 = new MK.Markov<int> (_testList, 1);

			Assert.AreEqual (markovChain1.ProbabilityOf (TR.Make (2), 3), .5);

			// Tests the chain of degree 2
			MK.Markov<int> markovChain2 = new MK.Markov<int>(_testList, 2);

			Assert.AreEqual (markovChain2.ProbabilityOf (TR.Make (0, 1), 7), .5);

			// Tests the chain of degree 4
			MK.Markov<int> markovChain4 = new MK.Markov<int>(_testList, 4);
			Assert.AreEqual (markovChain4.ProbabilityOf (TR.Make (0, 1, 2, 3), 2), 1.0);
		}
	}
}

