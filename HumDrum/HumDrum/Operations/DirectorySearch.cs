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
			Files = new List<string> ();

			Include (filepath, option);
		}

		/// <summary>
		/// Initializes a DirectorySearch containing multiple directories.
		/// </summary>
		/// <param name="filepaths">All of the directories to be included in this directorysearch</param>
		/// <param name="option">Option.</param>
		public DirectorySearch(string[] filepaths, SearchOption option)
		{
			Files = new List<string> ();

			foreach (string s in filepaths) 
				Include (s, option);
		}

		/// <summary>
		/// Include this directory in the resulting files
		/// </summary>
		/// <param name="directory">The directory to include</param>
		/// <param name="option">Whether or not to just scan the top directory lvel</param>
		public void Include(string directory, SearchOption option)
		{
			foreach(string name in Directory.EnumerateFiles(directory, "*", option))
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

