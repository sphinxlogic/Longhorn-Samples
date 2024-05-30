#/*+=========================================================================
#  File:       MAKEFILE
#
#  Summary:    Makefile for building the SmplHndlr.dll code sample application.
#
#              This Makefile creates a subdirectory (TEMP) for the
#              .OBJ and .RES files used during the build process.
#
#  Builds:     SmplHndlr.dll
#
#--Usage:-------------------------------------------------------------------
#  NMAKE Options
#
#  Use the table below to determine the additional options for NMAKE to
#  generate various application debugging, profiling and performance tuning
#  information.
#
#  Application Information Type         Invoke NMAKE
#  ----------------------------         ------------
#  For No Debugging Info                nmake nodebug=1
#  For Working Set Tuner Info           nmake tune=1
#  For Call Attributed Profiling Info   nmake profile=1
#
#  Note: The three options above are mutually exclusive (you may use only
#        one to compile/link the application).
#
#  Note: creating the environment variables NODEBUG, TUNE, and PROFILE
#        is an alternate method to setting these options via the nmake
#        command line.
#
#  Additional NMAKE Options             Invoke NMAKE
#  ----------------------------         ------------
#  For No ANSI NULL Compliance          nmake no_ansi=1
#    (ANSI NULL is defined as PVOID 0)
#  To compile for Unicode               nmake unicode=1
#    (Default is ANSI)
#  To clean up temporary binaries       nmake clean
#  To clean up all generated files      nmake cleanall
#
#---------------------------------------------------------------------------
#  This file is part of the Microsoft Code Samples.
#
#  Copyright (C) 2004 Microsoft Corporation.  All rights reserved.
#
#  This source code is intended only as a supplement to Microsoft
#  Development Tools and/or on-line documentation.  See these other
#  materials for detailed information regarding Microsoft code samples.
#
#  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
#  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
#  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
#  PARTICULAR PURPOSE.
#=========================================================================+*/

#  WIN32.MAK can be included at the front of any Win32 makefile.
#
#  Define APPVER = [ 3.50 | 3.51 | 4.0 ] prior to including win32.mak to get
#  build time checking for version dependencies and to mark the executable
#  with version information.
#
#  Define TARGETOS = [ WIN95 | WINNT | BOTH ] prior to including win32.mak
#  to get some build time checking for platform dependencies.
#
APPVER=4.0
TARGETOS=BOTH
!include <win32.mak>


# Assign the main program name macro.
PGM = smplhndlr

# Use a temporary sub-directory to store intermediate
# binary files like *.obj, *.map, etc.
TDIR = TEMP

LINK = $(link)

LINKFLAGS = $(ldebug)
CDBG=$(cdebug) -DUNICODE -D_UNICODE
RCFLAGS = -DWIN32 -DRC_INCLUDE -DUNICODE


# If NODEBUG is not defined then define DEBUG during Compiles.
# The default is to compile with debug symbols in the binaries.
!IFNDEF NODEBUG
CDBG = $(CDBG) -DDEBUG
RCFLAGS = $(RCFLAGS) -DDEBUG
ODIR = DEBUG
!ELSE
ODIR = RELEASE
!ENDIF

# APPLIBS are libraries used by this application that are outside
# of its indigenous file set and are needed during the final link.
APPLIBS = shell32.lib \
	comctl32.lib \
	shlwapi.lib

# PGMOBJS is a macro that defines the object files for this application.
PGMOBJS = $(TDIR)\*.obj

# The final target.
all: tempdir outdir output

# Make the temporary work sub-directory.
tempdir:
  -mkdir $(TDIR)

outdir:
  -mkdir $(ODIR)

# The actual output products.
output: $(ODIR)\$(PGM).dll

OBJ=$(TDIR)\dllmain.obj \
	$(TDIR)\cfact.obj \
	$(TDIR)\enum.obj \
	$(TDIR)\handler.obj \
	$(TDIR)\reg.obj \

OBJC=$(TDIR)\guid.obj

$(OBJ):

$(OBJC):

.cpp{$(TDIR)}.obj:
  $(cc) $(cvars) $(cflags) $(CDBG) -Fo$@ $<

.c{$(TDIR)}.obj:
  $(cc) $(cvars) $(cflags) $(CDBG) -Fo$@ $<


# Compile the resources.
$(TDIR)\$(PGM).res: handler.rc handler_24bit.ico handler.bmp resource.h
  rc $(RCFLAGS) -r -fo$@ handler.rc

# Link the object and resource binaries into the final target binary.
$(ODIR)\$(PGM).dll: $(OBJ) $(OBJC) smplhndlr.def $(TDIR)\$(PGM).res
  $(LINK) @<<
    $(LINKFLAGS)
    /DLL
    -out:$@
    /DEF:smplhndlr.def
    $(OBJ) $(OBJC)
    $(TDIR)\$(PGM).res
    $(olelibs) $(APPLIBS)
<<

# Target to clean up temporary binaries.
clean:
  -del $(PGM).pdb
  -rmdir /s /q $(TDIR)
  -rmdir /s /q debug
  -rmdir /s /q release
