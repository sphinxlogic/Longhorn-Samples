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
InstallPrintQueueOnLocalServerUsingAttributeValueDictionary(
    String*                  printerName,
    String*                  driverName,
    String*                  portName,
    String*                  printProcessorName,
    PrintQueueAttributes     attributes,
    String*                  shareName,
    String*                  comment,
    String*                  location,
    Int32                    priority
    )
{
    try
    {
		String* portNames[] = {portName};

        PrintSystemAttributeValueDictionary*
        printQueueAttributeValueCollection      =   new PrintSystemAttributeValueDictionary();

        PrintQueueAttributeAttributeValue*
        queueAttributesAttribute                =   new PrintQueueAttributeAttributeValue(S"QueueAttributes",
                                                                                          __box(attributes));
        PrintSystemStringAttributeValue*
        shareNameAttribute                      =   new PrintSystemStringAttributeValue(S"ShareName",
                                                                                        shareName);
        PrintSystemStringAttributeValue*
        commentAttribute                        =   new PrintSystemStringAttributeValue(S"Comment",
                                                                                        comment);
        PrintSystemStringAttributeValue*
        locationAttribute                       =   new PrintSystemStringAttributeValue("Location",
                                                                                        location);
        PrintSystemInt32AttributeValue*
        priorityAttribute                       =   new PrintSystemInt32AttributeValue("Priority",
                                                                                       __box(priority));

        printQueueAttributeValueCollection->Add(queueAttributesAttribute);
        printQueueAttributeValueCollection->Add(shareNameAttribute);
        printQueueAttributeValueCollection->Add(commentAttribute);
        printQueueAttributeValueCollection->Add(locationAttribute);
        printQueueAttributeValueCollection->Add(priorityAttribute);

        LocalPrintServer* printServer = new LocalPrintServer(PrintSystemDesiredAccess::AdministrateServer);

        PrintQueue* printQueue        = printServer->InstallPrintQueue(printerName,
                                                                      driverName,
                                                                      portNames,
                                                                      printProcessorName,
                                                                      printQueueAttributeValueCollection);

    }
    catch (PrintServerException* printException)
    {
        Console::WriteLine("{0}\n", printException->Message);
    }

	Console::ReadLine();
}
