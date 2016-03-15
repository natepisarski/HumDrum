Hey! You've found your way to the github repository for a general purpose C# library called HumDrum (meaning: dull. Or, in some cases, a bunch of stuff put together, like this library).

It contains tons of functions for the general purpose tasks you find yourself doing while programming. Its key goals involve having no external (that is - non .NET) dependencies, making extensive use of implementing and supporting functions which work on generics (more on this later),

# Version
At the time of this commit, the version is:
**1.0.0**
* First number: Major version. Breaks compatibility in some way.
* Second number: Minor version. Adds some kind of feature.
* Third number: Revision version. Bug, documentation, or test related changes.

# Branches
HumDrum is split up into 3 branches with any number of sub branches. Right now, these branches are: Operations, Structures, and Collections.

### Operations
Operations relates to IO or otherwise "impure" functionality. TCP stuff. Directory searching. Bitmap statistics. Things like that go in here.

### Structures
Structures, as its name suggests, relates to generic data structures such as binary trees and binding tables. These also contain the files which rely on such structures, but as of right now no such files exist.

### Collections
Collections is by far the largest branch of HumDrum. It relates to anything that involves functions on sets of data... That's a really generic definition, so you may be able to guess that this handles a LOT of stuff. Data interchange formats, pure sequence analysis, logic reductions, state-based grouping, etc.

## Further Reading
The MAP.md file quickly explains the purpose of each file in this library.

# Methodology
Why does something end up here? Well, basically, I just program as I normally would. Then, anything that doesn't pertain *directly* to the project I'm working on gets slapped into HumDrum. If you have something that you think would be a good fit in here, chances are it actually is a good fit - send a pull request.

# License
BSD 3-clause
