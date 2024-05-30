//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
//
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------

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
		               "FullPropertiesLocalPrintQueueOperations <PrintQueueName> ");
	Console::WriteLine("This would run with the following defaults:");
	Console::WriteLine("New User JobTicket Orientation: Portrait");
	Console::WriteLine("To change the defaults edit the application and compile");
}

void _tmain(int argc, char* argv[])
{
    try
    {
        Console::WriteLine("System.Printing Local PrintQueue Query subset of properties sample ...");
        if(argc<2)
        {
            WriteCommandArguments();
        }
		else
		{
			PrintQueueSample::LocalPrintQueueOperationsWithSubSetOfProperties(argv[1]);
		}
    }
    catch(PrintQueueException* e)
    {
        String* message = e->Message;
    }

	Console::ReadLine();
}
