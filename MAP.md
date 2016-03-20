# Hum Drum Project Map
This nifty document will guide you around the HumDrum library, describing both what each wing of the library does and what its sub-sections consist of.

## Collections
Collections is the wing of the HumDrum library that deals with any function performed on a series of (commonly) generic data.

### Markov
Markov is a subsection of collections that sets up the relevant data structure for working with Markov chains, a state-dependant prediction of future states.

#### MarkovState
Represents one item of a markov chain. Holds some kind of state, a future value, and the probability that the future state will occur when its state is reached.

#### Markov
Markov is the actual class for working with these chains. Its constructor takes a sequence of data which is automatically turned into a list of MarkovStates. When a state is given to this object, it can either return the most likely future states or a state at random (a-la many message generators).

### StateModifiers
StateModifiers is a library suite for HumDrum.Collections.Groups.StateObject<T> subclasses. These work as hodge-podgey implementations of state machines.

#### IntegerCounter
IntegerCounter is a pretty simple state machine. It takes a maximum value and a step-increase value (i.e, step increase value of 2 would make it go {2, 4, 6} etc.).

### Groups
Groups is a library for using a state machine (StateObject<T> defined in this class) to group information in a sequence.

### HigherOrder
HigherOrder is a library that mimics LINQ-style queries for generic collection structures. These functions allow various other kinds of functions to be passed in as a parameter to control how these work.

### Information
Information is a very general library. Any function that pertains to extracting existing elements of a list as-is, or providing metadata about the list itself will wind up here. This library is the "other half" of the Transformations library, but it does not type of manipulation to the list.

### Predicates
Predicates is a library for dealing with analysis on sequences of boolean values.

### Sections
Sections is a library for parsing text. It defines various ways to group and extract strings from source material.

### Transformations
Transformations is another general collections library. The "other half" of Information, Transformations defines ways to manipulate a list to turn it into a different structure. This is either a reordered list or another data structure entirely.

## Operations
Operations is the wing of the HumDrum library pertaining to IO, or some very particular task.

### Files
Files is a subsection of Operations that relates to operations of files / directories.

#### DirectorySearch
DirectorySearch is a library for searching directories recursively for files meeting some type of criteria. Using method cascading, heavy refining of included files is made very easy with DirectorySearch.

#### Line
Line is a simple class that represents the line of a file. Lines are wrapped in this class so that their parent file and line number can be referenced even after it is separated from such a context.

#### ISequentialWriter
SequentialWriter is an interface defining two things: the writing of a file with an extension and the naming of such a file based on the directory. In short, anything that scans the directory to determine what the next file should be named can be a SequentialWriter

#### NumericalWriter
NumericalWriter is a SequentialWriter that scans the directory for numbered files, starting at 0.

### ImageManager
A library for calculating image metadata. This includes functions such as the average color, most similar image based on color, and image searching.

### Logger
Logger writes information to an OutputStream. Logger is commonly used on FileOutputStreams to create log files.

### Servitor
Servitor is a server program that listens on a given port and buffers input. Then, the client can read information from this buffer. This wraps the shockingly low-level .NET network library.

## Recursion
Recursion is a suite of libraries that assist or make heavy use of recursive functions.

### TailHelper
TailHelper contains functions that assist in tail-recursion, especially in translating data.

## Structures
Structures is the wing of HumDrum that defines data structures.

### BindingsTable
BindingsTable contains functions for manipulating two lists of information, and manipulating lists of points. This can also be used a simple key-value database.

### Direction
A very simple enum for Up, Down, Left, Right.

### Tree
A simple implementation of a binary tree.

