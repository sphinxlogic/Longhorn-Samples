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
}

void _tmain(int argc, char* argv[])
{
    try
    {
		Console::WriteLine("System.Printing Inatalling a Print Queue sample ...");
        if(argc<3)
        {
            WriteCommandArguments();
        }
		else
		{
	        PrintServerSample::InstallPrintQueueOnLocalServer(argv[1],argv[2],argv[3],argv[4]);
		}
    }
    catch(PrintQueueException* e)
    {
        String* message = e->Message;
    }

	Console::ReadLine();
}
