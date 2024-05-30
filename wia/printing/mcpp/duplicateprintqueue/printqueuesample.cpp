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

#ifndef  __PRINTQUEUESAMPLE_HPP__
#include "PrintQueueSample.hpp"
#endif

using namespace SystemPrintingSample;


void
PrintQueueSample::
DuplicatePrintQueue(
    String* printServerName,
    String* printQueueName,
    String* duplicatedPrintQueueName
    )
{
    try
    {
        PrintServer* printServer = new PrintServer(printServerName,
                                                   PrintSystemDesiredAccess::AdministrateServer);

        PrintServer* printServerLocal = new PrintServer(PrintSystemDesiredAccess::AdministrateServer);

        String* printQueueProperties[] = {S"ShareName",
                                          S"Comment",
                                          S"Location",
                                          S"Priority",
                                          S"DefaultPriority",
                                          S"StartTime",
                                          S"UntilTime",
                                          S"AveragePpm",
                                          S"QueueAttributes",
                                          S"QueueDriver",
                                          S"QueuePort",
                                          S"QueuePrintProcessor",
                                          S"SepFile",
                                          S"DefaultJobTicket"};

        PrintQueue* toDuplicatePrintQueue = printServer->GetPrintQueue(printQueueName,
                                                                       printQueueProperties);

        String* ports[] = new String*[1];
        ports[0] = toDuplicatePrintQueue->QueuePort->Name;

		PrintQueue* duplicatePrintQueue = printServerLocal->InstallPrintQueue(duplicatedPrintQueueName,
                                                                              toDuplicatePrintQueue->QueueDriver->Name,
                                                                              ports,
                                                                              toDuplicatePrintQueue->QueuePrintProcessor->Name,
                                                                              toDuplicatePrintQueue->PropertiesCollection);
    }
    catch (PrintServerException* printException)
    {
        Console::WriteLine("{0}", printException->Message);
    }
}
