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
		private readonly CR[] _OrderedCardinals = {CR.NORTH, CR.EAST, CR.SOUTH, CR.WEST};

		/// <summary>
		/// Clockwise-Ordered directions, starting with Up
		/// </summary>
		private readonly DR[] _OrderedDirections = {DR.UP, DR.RIGHT, DR.DOWN, DR.LEFT};

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
			Assert.AreEqual (DR.UP, DO.TranslateCardinal (CR.NORTH));
			Assert.AreEqual (DR.RIGHT, DO.TranslateCardinal (CR.EAST));
			Assert.AreEqual (DR.DOWN, DO.TranslateCardinal (CR.SOUTH));
			Assert.AreEqual (DR.LEFT, DO.TranslateCardinal (CR.WEST));

			/* Directions */
		    Assert.AreEqual (CR.NORTH, DO.TranslateDirection (DR.UP));
			Assert.AreEqual (CR.EAST, DO.TranslateDirection (DR.RIGHT));
			Assert.AreEqual (CR.SOUTH, DO.TranslateDirection (DR.DOWN));
			Assert.AreEqual (CR.WEST, DO.TranslateDirection (DR.LEFT));
		}

		/// <summary>
		/// Tests all of the rotation functions
		/// </summary>
		[Test]
		public void TestRotate()
		{
			// Orbital Check
			Assert.AreEqual(DO.RotateOrbital(OD.CLOCKWISE), OD.COUNTERCLOCKWISE);
			Assert.AreEqual (DO.RotateOrbital (OD.COUNTERCLOCKWISE), OD.CLOCKWISE);

			for (int i = 0; i < 3; i++) {

				// Cardinal Check
				Assert.AreEqual (
					DO.RotateCardinal (_OrderedCardinals [i], OD.CLOCKWISE),
					IF.LoopGet<CR>(_OrderedCardinals, i+1));

				// Direction Check
				Assert.AreEqual (
					DO.RotateDirection (_OrderedDirections [i], OD.CLOCKWISE),
					IF.LoopGet<DR>(_OrderedDirections, i+1));
			}
		}
	}
}

