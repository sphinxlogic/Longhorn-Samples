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
#include "DefaultPrintQueueSample.h"
#endif

using namespace SystemPrintingSample;

void
DefaultPrintQueueSample::
DefaultPrintQueueOperations(
    void
    )
{
    try
    {
        Console::WriteLine("Sample code for Default Print Queue operations");
        //
        // Instantiating an instance of the Local Print Server and this is the only
        // way to get to the default print queue
        //
        LocalPrintServer* lclPrintServer         = new LocalPrintServer();
        //
        // Out of the Local Print Server Properties, we are only interested
        // in the Default Print Queue for this sample
        //
        PrintQueue*       defaultPrintQueue      = lclPrintServer->DefaultPrintQueue;
        //
        // Query different Properties of the Print Queue
        // HostingPrintServerName: If the default PrintQueue is a connection then this
        //                         points out to the server from which the PrintQueue is
        //                         shared out
        //
        String*           hostingPrintServerName = defaultPrintQueue->HostingPrintServer->Name;
        //
        // FullName              :This is the full \\Server\PrintQueue name
        //
        String*           printQueueFullName     = defaultPrintQueue->FullName;
        //
        // Querying the name of the PrintQueue
        //
        String*           name                   = defaultPrintQueue->Name;
        //
        // Querying the Port, Driver and Print Processor of the Print Queue
        //
        Port*             port                   = defaultPrintQueue->QueuePort;
        Driver*           driver                 = defaultPrintQueue->QueueDriver;
        PrintProcessor*   printProcessor         = defaultPrintQueue->QueuePrintProcessor;
        //
        // Querying the priority by which the jobs are scheduled on this print queue
        //
        Int32             priority               = defaultPrintQueue->Priority;
        //
        // Querying the number of Jobs queued on the PrintQueue
        //
        Int32             noJobs                 = defaultPrintQueue->NumberOfJobs;
        //
        // Querying othe properties of the PrintQueue like Comment,Location,SpeFile and ShareName
        //
        String*           queueLocation          = defaultPrintQueue->Location;
        String*           queueComment           = defaultPrintQueue->Comment;
        String*           separatorFile          = defaultPrintQueue->SepFile;
        String*           share                  = defaultPrintQueue->ShareName;
        String*           description            = defaultPrintQueue->Description;
        //
        // Querying the driver settings of the PrintQueue for both Printer and User
        //
        JobTicket*        defaultJobTicket       = new JobTicket(defaultPrintQueue->DefaultJobTicket);
        PrintSchema::
        OrientationValues orientationValueD      = defaultJobTicket->PageOrientation->Value;
        JobTicket*        userJobTicket          = new JobTicket(defaultPrintQueue->UserJobTicket);
        PrintSchema::
        OrientationValues orientationValueU      = userJobTicket->PageOrientation->Value;
        //
        // Print out the queried values
        //
        Console::WriteLine("Default PrintQueue: {0} on Print Server: {1} has the following properties:\n"
                           "Port:           {2}\nDriver:         {3}\nPrintProcessor: {4}\n"
                           "Location:       {5}\nComment:        {6}\nOrientation:    {7}\n"
                           "Description:    {8}\nSeperator_File: {9}\nShare_Name:     {10}\n"
                           "Number Of Jobs  {11}\nPaused:         {12}\n",
                           name,hostingPrintServerName,
                           port->Name,driver->Name,printProcessor->Name,
                           queueLocation,queueComment,
                           __box(orientationValueU)->ToString(),
                           description,separatorFile,share,
                           noJobs,defaultPrintQueue->IsPaused.ToString());

        //
        // If required, the latest settings could be retrieved from the Print Server once
        // more
        //
        defaultPrintQueue->Refresh();
    }
    //
    // Example of exceptions that can be thrown could be due to
    // access denied on trying to pause the Print Queue if you are
    // not an administrator on that queue
    //
    catch(PrintQueueException* printSystemException)
    {
        Console::WriteLine("Error Message {0}",
                           printSystemException->Message);

    }
    catch(PrintSystemException* printSystemException)
    {
        Console::WriteLine("Error Message {0}",
                           printSystemException->Message);
    }
}