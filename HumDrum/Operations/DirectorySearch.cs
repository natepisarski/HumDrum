using System;
using System.IO;
using System.Collections.Generic;

namespace HumDrum.Operations
{
	/// <summary>
	/// Searches directories for matching
	/// files
	/// </summary>
	public class DirectorySearch
	{
		/// <summary>
		/// The files
		/// </summary>
		/// <value>The files.</value>
		public List<string> Files {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.DirectorySearch"/> class.
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		/// <param name="option"> </param>
		public DirectorySearch (string filepath, SearchOption option)
		{
			foreach (string name in Directory.EnumerateFiles (filepath, "*", option))
				Files.Add (name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="files">Files.</param>
		protected DirectorySearch(List<string> files)
		{
			Files = files;
		}

		/// <summary>
		/// Refines the selection of files based on a predicate.
		/// </summary>
		/// <param name="refiner">File.</param>
		public DirectorySearch Refine(Predicate<string> refiner)
		{
			Files.RemoveAll (x => !refiner (x));
			return new DirectorySearch (Files);
		}
	}
}

