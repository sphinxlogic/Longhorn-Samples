#include "stdafx.h"

#using <mscorlib.dll>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

using namespace System::Printing;
using namespace System::Printing::PrintSubSystem;
using namespace System::Printing::Configuration;
using namespace Microsoft::Printing::JobTicket;

#ifndef  __PRINTSERVERSAMPLE_HPP__
#include "PrintServerSample.hpp"
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
		               "InstallPrintQueue <PrintQueueName> <DriverName> <PortName> <PrintProcessor> ");
	Console::WriteLine("This would install with some defaults");
	Console::WriteLine("To change the defaults edit the sample and re-compile");
}

void _tmain(int argc, char* argv[])
{
    try
    {
        Console::WriteLine("System.Printing Installing a Print Queue sample ...");
        if(argc<5)
        {
            WriteCommandArguments();
        }
		else
		{
	        PrintServerSample::
			InstallPrintQueueOnLocalServerUsingAttributeValueDictionary(argv[1],
																		argv[2],
																		argv[3],
																		argv[4],
																		PrintQueueAttributes::Published,
																		S"Sample_Shared",
																		S"Created with Attribute Value Collection",
																		S"In Virtual office",
																		3);
		}
    }
    catch(PrintQueueException* e)
    {
        String* message = e->Message;
    }

	Console::ReadLine();

}
