# HumDrum Library
The HumDrum library has only one job: try to make your life, as a developer, easier. Its strategy is to do this by letting you:

* Write the least amount of code possible
* Write the easiest code possible

Because HumDrum spans such a large number of use-cases, it's likely that it already has what you want in there. The MAP.md file gives a quick rundown of what you can find. Go give it a search.

## HumDrum makes simple things simpler
HumDrum tries to make code easier by providing a seamless experience, regardless of domain. The main way it accomplishes this is by **extending the HECK** out of base classes. For instance, **HumDrum.Collections** functions (of which there are currently a bit less than **100**) all work with **IEnumerable<>**, an interface that all of the common collections, like **List** and **Arrays** implement. This lets you use Length(), or ForEvery(), or any HumDrum.Collections function on ANY collection.

No more fumbling around with methods. "Does List have count or length? What is capacity?". Length() works the same no matter what you've got.

## HumDrum makes hard things simple
HumDrum, in addition to being a convenience library, is an experimental library. Portions of the library with the [Experimental] Attribute facilitate the use of new design patterns. Things such as having runtime-checked Traits, cached Network communication, image processing, and dynamic object building are all relatively simple matters in HumDrum.

Here are some examples of what the HumDrum library can do:

# Unification of IEnumerable functions

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

# LINQ-like syntax for any IEnumerable
````C#
// The sum of all even numbers plus one
var sum evenNumbersPlusOne = oneToTen
    .When(x => x % 2 == 0)
    .ForEvery(x => x + 1)
    .Reduce((x, y) => x + y);
````

# Convenience functions for system tasks
````C#
var systemScanner = new DirectorySearch ("/", System.IO.SearchOption.AllDirectories);

var executableThings = systemScanner
    .Refine (x => x.Contains (".exe"))
    .Refine (y => y.Contains ("thing")).Files;
    
````

# Dynamic Factory Generation with multiple constructors
````C#
ObjectBuilder obj = new ObjectBuilder(TestClass.GetType());

TestClass class = (TestClass) obj
	  .SetParameter(0, "x", "X-string")
	  .SetParameter(0, "y" 3)
	  .Instantiate();
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

# Database manipulation using the code-as-data model
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

## Markov Chains
````C#
// HumDrum.Collections.Markov
var markovChain = new HumDrum.Collections.Markov.Markov<int> (Transformations.Make (1, 2, 3, 1), 2);  
````

## Binary Trees
````C#
// HumDrum.Structures
Tree<int> tree = new Tree<int>(0);
var alphabet = HumDrum.Constants.LOWERCASE_EN_US_ALPHABET;  
````

## Direction manipulation

````C#
// HumDrum.Structures
var south = HumDrum.Structures.DirectionOperations.TranslateDirection (Direction.Down);
```

## And plenty more!

* The basis of plaintext parsing (**HumDrum.Collections.Sections**)
* List collection via State Machines (**HumDrum.Collections.Groups**)
* Custom Hashmap with LINQ-esque functionality (**HumDrum.Structures.BindingsTable**)
* Cached, Thread-safe, concurrent port listener (**HumDrum.Operations.Servitor**)
* Dynamic sequential file create (**HumDrum.Operations.Files.NumericalWriter**)
* Native Objects for Database objects, including a driver for SQL / Access / etc. bindings (**HumDrum.Operations.Database**)
* Image forensics

With HumDrum, you can have **all that fun** while having to write **none of the code**. It's a win-win!

# Installing HumDrum
Currently, there is no NuGet package for HumDrum. However, you can use the project file inside of the HumDrum directory here directly in your project. The source is open and under a non-viral license, so it's 100% legal and safe to do that. And you even get to modify / add something that's broken / not there.

A nuget package will be coming in the very near future though!

# Branches
HumDrum is split up into 4 branches with any number of sub branches. Right now, these branches are: Operations, Structures, Collections, and Traits.

### Operations
Operations relates to IO or otherwise "impure" functionality. TCP stuff. Directory searching. Bitmap statistics. Things like that go in here.

#### Files
Operations for dealing with files and the directory structure.

#### Database
Operations for working with database objects and interfacing with foreign databases, like SQL.

### Structures
Structures, as its name suggests, relates to generic data structures such as binary trees and binding tables. These also contain the files which rely on such structures, but as of right now no such files exist.

### Collections
Collections is by far the largest branch of HumDrum. It relates to anything that involves functions on sets of data... That's a really generic definition, so you may be able to guess that this handles a LOT of stuff. Data interchange formats, pure sequence analysis, logic reductions, state-based grouping, etc.

### Traits
Traits is an experimental branch of HumDrum that, really, should not be relied on. It's overdue for some hacking. Currently, it enables Trait-based programming by making its own definition for classes and interfaces, which is obviously pretty cumbersome. However, if your code REALLY needs Traits to operate, this branch gives you the ability to do that.

#Status
HumDrum is under active development, yet is now stable. There will only be scheduled breakages of code. Additions to the library happen very frequently. Same for tests and documentation. However, renaming and removals only happen on scheduled intervals.

# Version
At the time of this commit, the version is:
**3.0.0.0**
* First number: Major version. Breaks compatibility in some way.
* Second number: Minor version. Adds some kind of feature.
* Third number: Revision version. Some type of code or test change
* Fourth number: Documentation change, either inline, generated, or one of the files

## Further Reading
The MAP.md file quickly explains the purpose of each file in this library.

# License
BSD 3-clause
