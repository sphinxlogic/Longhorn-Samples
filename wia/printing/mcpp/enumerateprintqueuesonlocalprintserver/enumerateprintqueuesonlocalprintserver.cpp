#include "stdafx.h"

#using <mscorlib.dll>

using namespace System;
using namespace System::IO;
using namespace System::Collections;

using namespace System::Printing;
using namespace System::Printing::PrintSubSystem;
using namespace System::Printing::Configuration;
using namespace Microsoft::Printing::JobTicket;

#ifndef  __PRINTSERVERAMPLE_HPP__
#include "PrintServerSample.hpp"
#endif

using namespace SystemPrintingSample;

[STAThread]
void _tmain(int argc, char* argv[])
{
    try
    {
        Console::WriteLine("System.Printing enumerating PrintQueues on Local PrintServer sample ...");

	    PrintServerSample::EnumeratePrintQueuesOnLocalPrintServer();
    }
    catch(PrintQueueException* e)
    {
        String* message = e->Message;
    }

    Console::ReadLine();
}
