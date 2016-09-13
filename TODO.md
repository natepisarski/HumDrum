# Meta
Consider splitting HumDrum into separate Git repositories, or at least having a README.md in each branch and each branch being its own project.

# HumDrum.Operations.Database
* Add Database class that manages a collection of tables
* Add Row class that tables can return
* Add Intersection, Union, Difference, Cross Product, and Natural Join to Database
* Add `void ToFile(string)` to IDatabaseDriver

# HumDrum.Collections
* Consider moving Sections to a new branch (HumDrum.Experimental)

# HumDrum.Traits
* Consider moving entire branch under new branch (HumDrum.Experimental)

# HumDrum.Operations
* Make the Servitor unit tests actually work
* Remove the Logger class since it's not up-to-snuff with the rest of the library
