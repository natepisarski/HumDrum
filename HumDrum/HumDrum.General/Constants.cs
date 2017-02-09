using System;

namespace HumDrum
{
	[Stable]
	public class Constants
	{
		/// <summary>
		/// The uppercase A-Z alphabet
		/// </summary>
		public const string UPPERCASE_EN_US_ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		/// <summary>
		/// The Lower-Case A-Z Alphabet
		/// </summary>
		public const string LOWERCASE_EN_US_ALPHABET = "abcdefghijklmnopqrstuvwxyz";

		/// <summary>
		/// 0-9, in order.
		/// </summary>
		public readonly int[] DECIMAL_ASCENDING = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

		/// <summary>
		/// 9-0, in order
		/// </summary>
		public readonly int[] DECIMAL_DESCENDING = {9, 8, 7, 6, 5, 4, 3, 2, 1, 0};

		/// <summary>
		/// Months of the gregorian calendar, with the first letter capitalized.
		/// i.e "January"
		/// </summary>
		public readonly string[] GREGORIAN_MONTHS = {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
	}
}

