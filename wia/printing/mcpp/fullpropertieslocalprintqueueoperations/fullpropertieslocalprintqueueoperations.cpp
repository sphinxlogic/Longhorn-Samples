#include "stdafx.h"

#using <mscorlib.dll>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

using namespace System::Printing;
using namespace System::Printing::PrintSubSystem;
using namespace System::Printing::Configuration;
using namespace Microsoft::Printing::JobTicket;

#ifndef  __PRINTQUEUEAMPLE_HPP__
#include "PrintQueueSample.hpp"
#endif

using namespace SystemPrintingSample;

[STAThread]

void
WriteCommandArguments(
    void
    )
{
    Console::WriteLine(S"Incomplete command line arguments");
	Console::WriteLine("Enter:"
		               "FullPropertiesLocalPrintQueueOperations <PrintQueueName> "
					   "<NewPrintQueueName> <NewPortName>");
	Console::WriteLine("This would run with the following defaults:");
	Console::WriteLine("New Share Name:  SystemPrinting_Shared");
	Console::WriteLine("New Comment:     SystemPrinting PrintQueue");
	Console::WriteLine("New Orientation: Landscape");
	Console::WriteLine("To change the defaults edit the application and compile");
}

void _tmain(int argc, char* argv[])
{
    try
    {
        Console::WriteLine("System.Printing Local PrintQueue Query / Set sample ...");
        if(argc<4)
        {
            WriteCommandArguments();
        }
		else
		{
	        PrintQueueSample::LocalPrintQueueOperationsWithFullProperties(argv[1],
																		  argv[2],
																		  argv[3]);
		}
    }
    catch(PrintQueueException* e)
    {
		Console::WriteLine(e->Message);
    }

	Console::ReadLine();
}
