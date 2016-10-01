using System;

namespace HumDrum.Operations.Database
{
	public class SchemaAtom
	{
		/// <summary>
		/// The name of some particular column
		/// </summary>
		/// <value>The name of the column</value>
		public string ColumnName {get; set;}

		/// <summary>
		/// The type of some particular column
		/// </summary>
		/// <value>The column type</value>
		public Type ColumnType { get; set;}


		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.SchemaAtom"/> class.
		/// This will initialize a SchemaAtom for one column
		/// </summary>
		/// <param name="name">The name for this atom</param>
		/// <param name="type">The type for this item</param>
		public SchemaAtom (string name, Type type)
		{
			ColumnName = name;
			ColumnType = type;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.Database.SchemaAtom"/> class.
		/// This will use a Column and a particular row
		/// </summary>
		/// <param name="c">The column</param>
		public SchemaAtom(Column c) {
			ColumnName = c.Title;
			ColumnType = c.ColumnType;
		}
	}
}

