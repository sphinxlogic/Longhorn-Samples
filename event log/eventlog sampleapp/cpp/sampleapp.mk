INCDIR=$(INCLUDE);$(SDKDIR)\inc  
LIBDIR=$(LIB);$(SDKDIR)\lib

LIBPATH=$(LIBPATH);

CLFLAGS=/Zi  /GX -D_WIN32_WINNT=0x0600 -D_WIN32_IE=0x0605  
LIBS= advapi32.lib
      

SOURCE_FILES=SampleApp.cpp 
	     

all: SampleApp.exe

clean:
	del *.obj *.exe *.dll *.pdb *.ilk

#
# A sample application for Task Scheduler COM APIs
#
SampleApp.exe: $(SOURCE_FILES) $(RESOURCE_FILES)
	set INCLUDE=$(INCDIR)
	set LIB=$(LIBDIR)
	set LIBPATH=$(LIBPATH)
	cl $(CLFLAGS) /Fe$@ $(SOURCE_FILES) $(LIBS) $(LINKEROPTS) SampleApp.res
        