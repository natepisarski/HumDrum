using System;

namespace HumDrum.Collections
{
	/// <summary>
	/// The Equality Type specifies a certain type
	/// of Equality for collections. This is used for
	/// sequence comparisons defined in HumDrum.Collection.Information
	/// </summary>
	[Stable]
	public enum EqualityType
	{
		/// <summary>
		/// The list is exactly the same
		/// </summary>
		OneToOne,

		/// <summary>
		/// The members and number of occurences
		/// of each element are the only criteria
		/// </summary>
		Substantial,

		/// <summary>
		/// The set of members in one list should
		/// be the same as the other
		/// </summary>
		SetEquality
	}
}

