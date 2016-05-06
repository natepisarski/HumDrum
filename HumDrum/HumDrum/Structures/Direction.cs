using System;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	/// <summary>
	/// Enum representing the basic directions
	/// </summary>
	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	/// <summary>
	/// Enum representing cardinal directions
	/// </summary>
	public enum Cardinal
	{
		NORTH,
		SOUTH,
		EAST,
		WEST
	}

	/// <summary>
	/// Enum represnting orbital directions (counter and forward clockwise)
	/// </summary>
	public enum OrbitalDirection
	{
		CLOCKWISE,
		COUNTERCLOCKWISE
	}

	/// <summary>
	/// Class for translating between the two direction variants
	/// </summary>
	public class DirectionOperations
	{
		/// <summary>
		/// Translates the direction.
		/// </summary>
		/// <returns>The direction's cardinal equivelant</returns>
		/// <param name="direction">The direction to translate</param>
		public static Cardinal TranslateDirection(Direction direction)
		{
			Cardinal[] orderedCardinals = {Cardinal.NORTH, Cardinal.EAST, Cardinal.SOUTH, Cardinal.WEST};
			Direction[] orderedDirections = {Direction.UP, Direction.RIGHT, Direction.DOWN, Direction.LEFT};

			return Information.RelativeMembers (orderedDirections, direction, orderedCardinals).Get (0);
		}

		/// <summary>
		/// Translates the Cardinal into a Direction
		/// </summary>
		/// <returns>The Direction</returns>
		/// <param name="cardinal">The Cardinal</param>
		public static Direction TranslateCardinal(Cardinal cardinal)
		{
			Cardinal[] orderedCardinals = {Cardinal.NORTH, Cardinal.EAST, Cardinal.SOUTH, Cardinal.WEST};
			Direction[] orderedDirections = {Direction.UP, Direction.RIGHT, Direction.DOWN, Direction.LEFT};

			return Information.RelativeMembers (orderedCardinals, cardinal, orderedDirections).Get (0);
		}

		/// <summary>
		/// Inverts this OrbitalDirection
		/// </summary>
		/// <returns>Clockwise if given counterclockwise, Counterclockwise if given clockwise</returns>
		/// <param name="orbit">The original orbit</param>
		public static OrbitalDirection RotateOrbital(OrbitalDirection orbit)
		{
			return (orbit == OrbitalDirection.CLOCKWISE ? OrbitalDirection.COUNTERCLOCKWISE : OrbitalDirection.CLOCKWISE);
		}

		/// <summary>
		/// Rotates the direction a certain way, based on the second parameter
		/// </summary>
		/// <returns>The rotated direction</returns>
		/// <param name="beginning">The beginning direction</param>
		/// <param name="direction">The way in which to turn it</param>
		public static Direction RotateDirection(Direction beginning, OrbitalDirection direction)
		{
			switch (beginning) {
			case Direction.UP:
				return (direction == OrbitalDirection.CLOCKWISE ? Direction.RIGHT : Direction.LEFT);
			case Direction.RIGHT:
				return (direction == OrbitalDirection.CLOCKWISE ? Direction.DOWN : Direction.UP);
			case Direction.DOWN:
				return (direction == OrbitalDirection.CLOCKWISE ? Direction.LEFT : Direction.RIGHT);
			case Direction.LEFT:
				return (direction == OrbitalDirection.CLOCKWISE ? Direction.UP : Direction.DOWN);
			}

			// Default. Should never happen. Ever.
			return Direction.UP;
		}

		/// <summary>
		/// Rotates the Cardinal direction a certain way, based on the second parameter
		/// </summary>
		/// <returns>The cardinal direction after rotation</returns>
		/// <param name="cardinal">The starting cardinal direction</param>
		/// <param name="direction">The direction in which to move it</param>
		public static Cardinal RotateCardinal(Cardinal cardinal, OrbitalDirection direction)
		{
			return DirectionOperations.TranslateDirection (
				DirectionOperations.RotateDirection (
					DirectionOperations.TranslateCardinal (cardinal),
					direction));
		}
	}
}