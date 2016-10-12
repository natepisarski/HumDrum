using System;

using HumDrum.Collections;

namespace HumDrum.Structures
{
	/// <summary>
	/// Enum representing the basic directions
	/// </summary>
	public enum Direction
	{
		Up,
		Down,
		Left,
		Right
	}

	/// <summary>
	/// Enum representing cardinal directions
	/// </summary>
	public enum Cardinal
	{
		North,
		South,
		East,
		West
	}

	/// <summary>
	/// Enum represnting orbital directions (counter and forward clockwise)
	/// </summary>
	public enum OrbitalDirection
	{
		Clockwise,
		CounterClockwise
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
			Cardinal[] orderedCardinals = {Cardinal.North, Cardinal.East, Cardinal.South, Cardinal.West};
			Direction[] orderedDirections = {Direction.Up, Direction.Right, Direction.Down, Direction.Left};

			return Information.RelativeMembers (orderedDirections, direction, orderedCardinals).Get (0);
		}

		/// <summary>
		/// Translates the Cardinal into a Direction
		/// </summary>
		/// <returns>The Direction</returns>
		/// <param name="cardinal">The Cardinal</param>
		public static Direction TranslateCardinal(Cardinal cardinal)
		{
			Cardinal[] orderedCardinals = {Cardinal.North, Cardinal.East, Cardinal.South, Cardinal.West};
			Direction[] orderedDirections = {Direction.Up, Direction.Right, Direction.Down, Direction.Left};

			return Information.RelativeMembers (orderedCardinals, cardinal, orderedDirections).Get (0);
		}

		/// <summary>
		/// Inverts this OrbitalDirection
		/// </summary>
		/// <returns>Clockwise if given counterclockwise, Counterclockwise if given clockwise</returns>
		/// <param name="orbit">The original orbit</param>
		public static OrbitalDirection RotateOrbital(OrbitalDirection orbit)
		{
			return (orbit == OrbitalDirection.Clockwise ? OrbitalDirection.CounterClockwise : OrbitalDirection.Clockwise);
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
			case Direction.Up:
				return (direction == OrbitalDirection.Clockwise ? Direction.Right : Direction.Left);
			case Direction.Right:
				return (direction == OrbitalDirection.Clockwise ? Direction.Down : Direction.Up);
			case Direction.Down:
				return (direction == OrbitalDirection.Clockwise ? Direction.Left : Direction.Right);
			case Direction.Left:
				return (direction == OrbitalDirection.Clockwise ? Direction.Up : Direction.Down);
			}

			// Default. Should never happen. Ever.
			return Direction.Up;
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