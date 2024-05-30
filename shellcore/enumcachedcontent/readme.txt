Offline Files Cache Enumeration Sample

This sample enumerates the files that are presently in the offline files cache
and displays a list of them.

To compile:

Compile the sample by loading cscenum.sln file into Visual Studio.
From the Build menu, choose Build Solution.

The sample is also supplied with an LHSDK MAKEFILE.  You can build the sample
from the command line using this makefile by simply running the NMAKE command
in the LHSDK Build Environment Window.  An alternate way to build this way is
to use the MSBUILD command as follows:

    MSBUILD cscenum.proj


To run:

The project builds a cscenum.exe file in its Release folder.
Run this file to list any files cached in the offline files cache.

Note that you may not have any files cached. In that case,
the command window will briefly appear then close.



