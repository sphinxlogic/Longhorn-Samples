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

//
// Printing namespaces
//
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
PointAndPrintConnectionSample(
    String* printServerName,
    String* printQueueName
    )
{
    try
    {
        Console::WriteLine("Creating a Connection to PrintQueue {0} on PrintServer {1}",
                           printQueueName,
                           printServerName);

        LocalPrintServer* localPrintServer = new LocalPrintServer();

        PrintQueue* remotePrintQueue = new PrintQueue(new PrintServer(printServerName),
                                                      printQueueName);

        //
        // Create a Connection
        //
        Boolean operationSucceeded   = localPrintServer->ConnectToPrintQueue(remotePrintQueue);

        if (operationSucceeded)
        {
            //
            // Set Connection as default PrintQueue
            //
            localPrintServer->DefaultPrintQueue = remotePrintQueue;

            localPrintServer->Commit();

            Console::WriteLine("Connection created and default printer switched to connection");
        }

    }
    catch (PrintSystemException* printException)
    {
        Console::Write("{0}\n", printException->Message);
    }
}
