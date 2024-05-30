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

#ifndef  __PRINTSERVERSAMPLE_HPP__
#include "PrintServerSample.hpp"
#endif

using namespace SystemPrintingSample;

void
PrintServerSample::
InstallPrintQueueOnLocalServer(
    String*   printQueueName,
    String*   driverName,
    String*   portName,
    String*   printProcessor
    )
{
    try
    {
        LocalPrintServer* lclPrintServer = new LocalPrintServer(PrintSystemDesiredAccess::AdministrateServer);
		String* portNames[] = {portName};

        PrintQueue* printQueue = lclPrintServer->InstallPrintQueue(printQueueName,
                                                                   driverName,
                                                                   portNames,
                                                                   printProcessor,
																   PrintQueueAttributes::Published);
		Console::WriteLine(S"PrintQueue Installed successfully");

    }
    catch (PrintServerException* printException)
    {
        Console::Write("{0}\n", printException->Message);
    }
}
