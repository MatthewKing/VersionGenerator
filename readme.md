VersionGenerator
================

A simple library and associated [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) that can be used to automatically generate version numbers in a variety of formats.

What version formats can it produce?
------------------------------------

This project is still a work in progress, so there are only three formats available at the moment.

In addition, I haven't come up with names for them yet, so they are currently just called `A`, `B`, and `C`.

A: This is the same algorithm as Microsoft's automatic assembly versioning in .NET (you know, the one you get when you use a wildcard in your AssemblyVersionAttribute... `[AssemblyVersion("1.0.*")]`).
B: This is a MSI-friendly way of encoding the current date into a version, with all the restrictions that Windows Installer places on version numbers. I'll write more on this method later.
C: This is another take on the algorithm above. It has a lower resolution, but allows you to set the major version. I'll write more on this method later.

Quickstart
----------

VersionGenerator is available both as a dotnet tool, and as a standard library.

## Using the tool:

1) Install the tool:

```
dotnet tool install -g VersionGenerator.Tool
```

2) Run the tool:

```
vgen A --timestamp now --major 1 --minor 0
```

### Using the library.

1) Install the NuGet pacakge:

```
PM> Install-Package VersionGenerator
```

or

```
dotnet add package VersionGenerator
```

2) Call the relevant API:

```csharp
var version = VersionTypeA.GenerateFromTimestamp(timestamp: DateTimeOffset.Now, major: 1, minor: 0);
```

License and copyright
---------------------

Copyright Matthew King 2019.
Distributed under the [MIT License](http://opensource.org/licenses/MIT).
Refer to license.txt for more information.
