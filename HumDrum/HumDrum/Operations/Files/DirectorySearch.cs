using System;
using System.IO;
using System.Collections.Generic;

using HumDrum.Collections;

namespace HumDrum.Operations.Files
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
		/// The subdirectories within this DirectorySearch's root direcory
		/// </summary>
		/// <value>The directories</value>
		public List<string> Directories { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="HumDrum.Operations.DirectorySearch"/> class.
		/// </summary>
		/// <param name="filepath">Filepath.</param>
		/// <param name="option"> </param>
		public DirectorySearch (string filepath, SearchOption option)
		{
			Files = new List<string> ();
			Directories = new List<string> ();

			Include (filepath, option);
		}

		/// <summary>
		/// Initializes a DirectorySearch containing multiple directories.
		/// </summary>
		/// <param name="filepaths">All of the directories to be included in this directorysearch</param>
		/// <param name="option">Option.</param>
		public DirectorySearch(IEnumerable<string> filepaths, SearchOption option)
		{
			Files = new List<string> ();
			Directories = new List<string> ();

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
			// Only process directories
			if (!File.GetAttributes (directory).HasFlag (FileAttributes.Directory))
				return;
			
			foreach(string name in Directory.EnumerateFiles(directory, "*", option))
				Files.Add (name);

			foreach (string name in Directory.EnumerateDirectories(directory, "*", option))
				Directories.Add (name);
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

		/// <summary>
		/// Goes through all of the files in this DirectorySearch,
		/// returning Line objects that match a certain string predicate
		/// </summary>
		/// <returns>The matching Line objects</returns>
		/// <param name="predicate">A predicate working on lines from a file</param>
		public IEnumerable<Line> LinesWhere(Predicate<string> predicate)
		{
			foreach (string filename in Files) 
				foreach (Line line in Line.AllLines(filename))
					if (predicate (line.Text))
						yield return line;
			yield break;
		}

		/// <summary>
		/// Get all of the lines in this DirectorySearch
		/// </summary>
		public IEnumerable<Line> Lines()
		{
			return LinesWhere (x => true);
		}

		/// <summary>
		/// Check to see whether or not this DirectorySearch has the given file in its files
		/// </summary>
		/// <returns><c>true</c> if this instance has filename; otherwise, <c>false</c>.</returns>
		/// <param name="filename">Filename.</param>
		public bool Has(string filename)
		{
			return Files.Contains (filename);
		}

		/// <summary>
		/// Gets a random file from the files that this DirectorySearch controls
		/// </summary>
		/// <returns>The filename</returns>
		public string RandomFile()
		{
			return Files.Get (new Random ().Next (Files.Length () - 1));
		}

		/// <summary>
		/// Determines whether or not the filename is a directory
		/// </summary>
		/// <returns><c>true</c> if is directory the specified filename; otherwise, <c>false</c>.</returns>
		/// <param name="filename">The path to the file</param>
		public static bool IsDirectory(string filename)
		{
			if (!File.Exists (filename))
				return false;
			
			return File.GetAttributes (filename).HasFlag (FileAttributes.Directory);
		}
	}
}

