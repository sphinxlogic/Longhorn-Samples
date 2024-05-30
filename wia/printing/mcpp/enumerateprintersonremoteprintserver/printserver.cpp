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
EnumeratePrintQueuesOnRemotePrintServer(
	String* serverName
	)
{
	try
    {
        Console::WriteLine("Sample code for enumerating Print Queues on Remote Print Server");

		PrintQueueProperty printerProperties[] = {PrintQueueProperty::Name,
												  PrintQueueProperty::QueueDriver,
												  PrintQueueProperty::QueuePort,
											   	  PrintQueueProperty::Comment ,
                                                  PrintQueueProperty::Location,
                                                  PrintQueueProperty::ShareName
											     };

        PrintServer* printServer = new PrintServer(serverName);

        PrintQueueCollection* printQueues = printServer->GetPrintQueues(printerProperties);


        IEnumerator* enumerator = printQueues->GetEnumerator();

        for(;enumerator->MoveNext();)
        {
			PrintQueue* printQueue = __try_cast<PrintQueue*>(enumerator->Current);
            String*           name                   = printQueue->Name;
            Port*             port                   = printQueue->QueuePort;
            Driver*           driver                 = printQueue->QueueDriver;
            String*           queueLocation          = printQueue->Location;
            String*           queueComment           = printQueue->Comment;
			String*           shareName              = printQueue->ShareName;
            //
            // Print out the queried values
            //
            Console::WriteLine("PrintQueue: {0} on Print Server: {1} has the following properites:\n"
                               "Port:        {2}\n"
                               "Driver:      {3}\n"
                               "Location:    {4}\n"
                               "Comment:     {5}\n"
							   "Shared as:   {6}\n",
                               name,
							   serverName,
                               port->Name,
                               driver->Name,
                               queueLocation,
                               queueComment,
							   shareName);
		}
	}
    catch (PrintSystemException* printException)
    {
		Console::WriteLine(printException->Message);
	}
}
