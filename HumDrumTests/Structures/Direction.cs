using System;
using NUnit.Framework;

using IF = HumDrum.Collections.Information;

using DO = HumDrum.Structures.DirectionOperations;

using DR = HumDrum.Structures.Direction;
using CR = HumDrum.Structures.Cardinal;
using OD = HumDrum.Structures.OrbitalDirection;

namespace HumDrumTests.Structures
{
	[TestFixture]	
	public class Direction
	{
		/// <summary>
		/// Clockwise-ordered cardinals, starting at North.
		/// </summary>
		private readonly CR[] _OrderedCardinals = {CR.North, CR.East, CR.South, CR.West};

		/// <summary>
		/// Clockwise-Ordered directions, starting with Up
		/// </summary>
		private readonly DR[] _OrderedDirections = {DR.Up, DR.Right, DR.Down, DR.Left};

		/// <summary>
		/// Set up this instance
		/// </summary>
		[SetUp]
		public void Setup()
		{

		}

		/// <summary>
		/// Tests all of the translation functions
		/// </summary>
		[Test]
		public void Translate()
		{
			/* Cardinals */
			Assert.AreEqual (DR.Up, DO.TranslateCardinal (CR.North));
			Assert.AreEqual (DR.Right, DO.TranslateCardinal (CR.East));
			Assert.AreEqual (DR.Down, DO.TranslateCardinal (CR.South));
			Assert.AreEqual (DR.Left, DO.TranslateCardinal (CR.West));

			/* Directions */
		    Assert.AreEqual (CR.North, DO.TranslateDirection (DR.Up));
			Assert.AreEqual (CR.East, DO.TranslateDirection (DR.Right));
			Assert.AreEqual (CR.South, DO.TranslateDirection (DR.Down));
			Assert.AreEqual (CR.West, DO.TranslateDirection (DR.Left));
		}

		/// <summary>
		/// Tests all of the rotation functions
		/// </summary>
		[Test]
		public void TestRotate()
		{
			// Orbital Check
			Assert.AreEqual(DO.RotateOrbital(OD.Clockwise), OD.CounterClockwise);
			Assert.AreEqual (DO.RotateOrbital (OD.CounterClockwise), OD.Clockwise);

			for (int i = 0; i < 3; i++) {

				// Cardinal Check
				Assert.AreEqual (
					DO.RotateCardinal (_OrderedCardinals [i], OD.Clockwise),
					IF.LoopGet<CR>(_OrderedCardinals, i+1));

				// Direction Check
				Assert.AreEqual (
					DO.RotateDirection (_OrderedDirections [i], OD.Clockwise),
					IF.LoopGet<DR>(_OrderedDirections, i+1));
			}
		}
	}
}

