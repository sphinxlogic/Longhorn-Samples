// StdAfx.h: Precompiled header file

#pragma once

#include "forcebuild.h"		// This forces a rebuild of the C++ code every build

#define _ASHEADER
#include "MyApp.g.cpp"			// Defines the base application object (this must always come first)
#include "MyApp.xaml.h"			// Defines the code behind class for the application object
#include "MainWindow.g.cpp"		// Defines the base window object
#include "MainWindow.xaml.h"	// Defines the code behind class for the window object

// TODO: add additional #includes for xaml file outputs here
//       the convention is <xaml file name>.g.cpp.
// TODO: add additional code behind files here
//       the convention is <xaml file name>.xaml.h

#include "animateproperty_mcpp.Main.g.cpp"	// Defines the entry point and resource loader (this must always come last)
#undef _ASHEADER
