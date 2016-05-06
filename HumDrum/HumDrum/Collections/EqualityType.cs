using System;

namespace HumDrum.Collections
{
	/// <summary>
	/// The Equality Type specifies a certain type
	/// of Equality for collections
	/// </summary>
	public enum EqualityType
	{
		/// <summary>
		/// The list is exactly the same
		/// </summary>
		ONE_TO_ONE,

		/// <summary>
		/// The members and number of occurences
		/// of each element are the only criteria
		/// </summary>
		SUBSTANTIAL,

		/// <summary>
		/// The set of members in one list should
		/// be the same as the other
		/// </summary>
		SET_EQUALITY
	}
}

