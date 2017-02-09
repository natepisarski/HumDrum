using System;

namespace HumDrum
{
	/// <summary>
	/// Stable is a promise that this section of the library will retain 
	/// compatibility until the next major version. However, this does not mean
	/// that new methods, code changes, and documentation changes will not be introduced.
	/// 
	/// It simply means that all classes and methods which currently exist within it will
	/// continue working as they are until the next major version.
	/// </summary>
	public class Stable : Attribute
	{

	}


	/// <summary>
	/// Experimental is used to signify a part of the library that is 
	/// new and may have minimal use / unit testing associated with it.
	/// 
	/// These areas of the library are subject to change, including:
	/// Broken compatibility, additions, deletions
	/// </summary>
	public class Experimental : Attribute 
	{

	}
}

