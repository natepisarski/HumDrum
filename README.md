# HumDrum Library
HumDrum is a general-purpose C# library I developed for use in personal real-world projects, and for fun.

HumDrum spans a large number of use-cases, but its greatest utility comes from its List processing capabilities that work on any `IEnumerable`.

Now, you don't have to wonder if it's `Array` or `List` that has `.capacity` or `.count` - you can just use `.Length`

There are almost 100 LINQ-style functions that work on any `IEnumerable` that make complicated transformations more simple.

## Cool Stuff
Since this package was mostly developed for my own enjoyment, it has a bunch of "cool stuff" that has no logical reason to be bundled in. Runtime-checked traits in native C#, a network caching layer, image processing, object factories, markov chains, and many other random facilities.

For the extra zany ones, you will find an `[Experimental]` attribute on them.

Here are some examples of what the HumDrum library can do:

# Unification of IEnumerable functions

````C#
// Let's make a test list
var oneToTen = HumDrum.Collections.Transformations.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10);   

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

# And a bunch of other stuff!

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

```C#
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
* Image utilities

# Installing HumDrum
Currently, there is no NuGet package for HumDrum. However, you can use the project file inside of the HumDrum directory here directly in your project. The source is open and under a non-viral license, so it's 100% legal and safe to do that. And you even get to modify / add something that's broken / not there.

# Branches
HumDrum is split up into 4 branches with any number of sub branches. Right now, these branches are: **Operations**, **Structures**, **Collections**, and **Traits**.

### Operations
"Impure" operations. IO, TCP/IP stuff, Directories, image manipulation, etc.

#### Files
Operations for dealing with files and the directory tree.

#### Database
Operations for working with database objects and interfacing with foreign databases, like SQL or XML datastores.

### Structures
Structures, as its name suggests, relates to generic data structures such as binary trees and binding tables.

### Collections
Collections is by far the largest branch of HumDrum. It relates to anything that involves functions on sets of data... That's a really generic definition, so you may be able to guess that this handles a LOT of stuff. Data interchange formats, pure sequence analysis, reducers, state-based grouping, etc.

### Traits
Enables aspect-oriented programming in pure C# with traits. Traits function more similar to Rust traits or Haskell typeclasses than PHP ones, meaning they are interfaces on steroids.

# Status
HumDrum was a large (and successful!) pet project of mine, but is no longer under active development.

## Further Reading
The MAP.md file quickly explains the purpose of each file in this library.

# License
BSD 3-clause
