using System;

namespace HumDrum.Operations.Database
{
	/// <summary>
	/// A RowItem is a binding used in Rows. In
	/// the HumDrum Database suite, Rows are not proper
	/// objects outright, but rather an amalgation of items
	/// in columns. As such, each RowItem contains a reference 
	/// to its parent column, its name type, and its item.
	/// </summary>
	[Experimental]
	public class RowItem
	{
		/// <summary>
		/// The type of this item in the row
		/// </summary>
		/// <value>The type of this particular item</value>
		public Type ItemType { get; private set;}

		/// <summary>
		/// The actual item from this row
		/// </summary>
		/// <value>The item.</value>
		public Object Item { get; private set;}

		/// <summary>
		/// The parent column for this Item
		/// </summary>
		/// <value>The parent column reference</value>
		public Column Parent {get; private set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.RowItem`1"/> class.
		/// This fills in the attributes for this RowItem, including the type.
		/// </summary>
		/// <param name="theType"> The type to use for this database</param>
		/// <param name="item">The item to use in this item</param>
		/// <param name="parent">The parent column used for this instance</param>
		public RowItem (Type theType, Object item, Column parent) 
		{
			ItemType = theType;

			Item = item;
			Parent = parent;
		}
	}
}

