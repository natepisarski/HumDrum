# Hum Drum
If you wanna see this library in action, take a quick look at these examples. Promise it won't be that hard to read!

# List Manipulation and IEnumerable extension

````C#
// Let's make a test list
var oneToTen = HumDrum.Collections.Transformations.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10);  

// HumDrum.Collections.Transformations.Genericize
IEnumerable<int> list = oneToTen.Genericize ();  

// HumDrum.Collections.Transformations.DropLast
var oneToNine = list.DropLast ();  

// HumDrum.Collections.Predicates.DoTo
var number6 = list.DoTo (x => (x == 5), (x => (x + 1)));  

// HumDrum.Collections.Information.Get
var number5 = list.Get (4);  

// HumDrum.Collections.Transformations.RemoveAt
var twoToNine = list.RemoveAt (0);  

// HumDrum.Collections.Transformations.Subsequence
var threeToSeven = list.Subsequence (2, 6);  
````

# Convenience functions for system tasks
````C#
/* Convenience functions for system tasks */
var systemScanner = new DirectorySearch ("/", System.IO.SearchOption.AllDirectories);
var executableThings = systemScanner.Refine (x => x.Contains (".exe")).Refine (y => y.Contains ("thing")).Files;
````

# A Naive impplementation of Traits
````C#
// Outside of the class: interface exampleInterface { void someCode(int x); }
// HumDrum.Traits
Interface exampleSupplement = new Interface (typeof(exampleInterface)); 

// HumDrum.Traits
Class implementor = new Class (typeof(Object));
implementor.AddMethod(new Method(new Action<int>(x => {}).Method, "someCode"));  

// HumDrum.Traits
Trait writable = new Trait (exampleSupplement, implementor);  

// HumDrum.Traits
writable.IsSatisfied(); // True
````

# Table manipulation using the code-as-data model
````C#
// Creates a table
			Table t = TableBuilder.Start ()
				.AddColumn (
				          ColumnBuilder<int>.Start ()
					.Title ("firstRow")
					.AddData (0)
					.AddData (1)
					.AddData (2)
					.Finalize ())
				.AddColumn (
				          ColumnBuilder<int>.Start ()
					.Title ("secondRow")
					.AddData (3)
					.AddData (4)
					.AddData (5)
					.AddData (6)
					.Finalize ()).Finalize();
````

# And just about anything else 
````C#
// HumDrum.Collections.Markov
var markovChain = new HumDrum.Collections.Markov.Markov<int> (Transformations.Make (1, 2, 3, 1), 2);  

// HumDrum.Structures
Tree<int> tree = new Tree<int>(0);
var alphabet = HumDrum.Constants.LOWERCASE_EN_US_ALPHABET;  

// HumDrum.Structures
var south = HumDrum.Structures.DirectionOperations.TranslateDirection (Direction.DOWN);

/*
* License : BSD 3-clause
* Author: Nathaniel Pisarski
* */
````

HumDrum in a hundred letters or less is this:

Anything you could want in C#. If the function isn't domain-specific, it winds up in HumDrum.

So, as a result, it has code from a whole bunch of different domains. Image processing, directory searching, statistical analysis, light AI stuff, collections manipulation, tree traversal, traits, whatever you want.

To deal with the breadth of what winds up in HumDrum, it's split up into a few branches.

# Branches
HumDrum is split up into 4 branches with any number of sub branches. Right now, these branches are: Operations, Structures, Collections, and Traits.

### Operations
Operations relates to IO or otherwise "impure" functionality. TCP stuff. Directory searching. Bitmap statistics. Things like that go in here.

### Structures
Structures, as its name suggests, relates to generic data structures such as binary trees and binding tables. These also contain the files which rely on such structures, but as of right now no such files exist.

### Collections
Collections is by far the largest branch of HumDrum. It relates to anything that involves functions on sets of data... That's a really generic definition, so you may be able to guess that this handles a LOT of stuff. Data interchange formats, pure sequence analysis, logic reductions, state-based grouping, etc.

### Traits
Traits is an experimental branch of HumDrum that, really, should not be relied on. It's overdue for some hacking. Currently, it enables Trait-based programming by making its own definition for classes and interfaces, which is obviously pretty cumbersome. However, if your code REALLY needs Traits to operate, this branch gives you the ability to do that.

#Status
HumDrum is under active development. As such, there are breakages. However, these breakages almost never effect code that is already unit tested (which is the vast majority of the library, including all of Collections).

Almost every week, new functions get put into the library. This should (in theory) kick the minor version number up. These additions are obviously not breaking existing code.

As for removals, well... Almost nothing gets removed. If I deem something to be pretty straight-up awful, it gets taken out. That almost never happens, as I said. 

Modifications to code happen. When these happen, it's usually when I'm cleaning things up, so there should be unit tests to reflect the change, which make it a bit safer than just going in and toying with things blind.

So, what I'm saying is - yes. You won't have any trouble relying on HumDrum. Although, I'm not vouching for its stability.

# Version
At the time of this commit, the version is:
**1.3.1**
* First number: Major version. Breaks compatibility in some way.
* Second number: Minor version. Adds some kind of feature.
* Third number: Revision version. Bug, documentation, or test related changes.

## Further Reading
The MAP.md file quickly explains the purpose of each file in this library. It tends to lag behind the actual directory structure, though.

# License
BSD 3-clause
