#
# Makefile used by nmake.exe
#

# Environment variables

# Assembly msjobs.dll must be in the reference path

SDKDIR=%WINDIR%\Microsoft.Net\Windows\v6.0.4030

REF_PATH=$(SDKDIR)

#LINKEROPTS=

LIBS=mscoree.lib

all: cs_Sample1.exe

clean:
	del *.obj *.exe *.dll *.pdb

#
# A sample application for Task Scheduler Managed INterface
#
CS_Sample1.exe: main.cs
	csc /debug+ /r:$(REF_PATH)\msjobs.dll /out:$(@B).exe main.cs
