#
# Makefile used by nmake.exe
#

# Environment variables

# Jobbuilder.h and jobsitf.h must be in the include directory.
# jobbuilder.lib and jobsitf.lib must be in the lib directory.

SDKDIR=C:\testdir\samples

INCDIR=$(INCLUDE);$(SDKDIR)\inc
LIBDIR=$(LIB);$(SDKDIR)\lib

LIBPATH=$(LIBPATH);

CLFLAGS=/Zi /D "_WIN32_DCOM" /GX
#LINKEROPTS=

LIBS= ole32.lib \
      oleaut32.lib \
      jobbuilder.lib \
      jobsitf.lib

SOURCE_FILES=main.cpp


all: CS_JobsSample.exe

clean:
	del *.obj *.exe *.dll *.pdb *.ilk

#
# A sample application for Task Scheduler COM APIs
#
CS_JobsSample.exe: $(SOURCE_FILES)
	set INCLUDE=$(INCDIR)
	set LIB=$(LIBDIR)
	set LIBPATH=$(LIBPATH)
	cl $(CLFLAGS) /Fe$@ $(SOURCE_FILES) $(LIBS) $(LINKEROPTS)
