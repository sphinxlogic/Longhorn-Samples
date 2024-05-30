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
LocalPrintQueueOperationsWithFullProperties(
    String*     printQueueName,
	String*     newPrintQueueName,
	String*     newPortName
    )
{
    PrintQueue* localPrintQueue = NULL;

    try
    {
        Console::WriteLine("Sample code for Local Print Queue operations");
        //
        // Instantiating an instance of the Local Print Queue with all possible
        // properties populated. Changing some of the properties on the PrintQueue
        // mendates instantiating with a PrinterFullAccess access. Also having this
        // access would allow the sample to Pause / Resume Print Queues
        //
        localPrintQueue = new PrintQueue(new PrintServer(),
                                         printQueueName,
                                         PrintSystemDesiredAccess::PrinterFullAccess);
        //
        // Querying the name of the PrintQueue
        //
        String*           name                   = localPrintQueue->Name;
        //
        // Querying the Port, Driver and Print Processor of the Print Queue
        //
        Port*             port                   = localPrintQueue->QueuePort;
        Driver*           driver                 = localPrintQueue->QueueDriver;
        PrintProcessor*   printProcessor         = localPrintQueue->QueuePrintProcessor;
        //
        // Querying the priority by which the jobs are scheduled on this print queue
        //
        Int32             priority               = localPrintQueue->Priority;
        //
        // Querying the number of Jobs queued on the PrintQueue
        //
        Int32             noJobs                 = localPrintQueue->NumberOfJobs;
        //
        // Querying othe properties of the PrintQueue like Comment,Location,SpeFile and ShareName
        //
        String*           queueLocation          = localPrintQueue->Location;
        String*           queueComment           = localPrintQueue->Comment;
        String*           seperatorFile          = localPrintQueue->SepFile;
        String*           share                  = localPrintQueue->ShareName;
        String*           description            = localPrintQueue->Description;
        //
        // Querying the driver settings of the PrintQueue for both PrintQueue and User
        //
        JobTicket*        defaultJobTicket       = new JobTicket(localPrintQueue->DefaultJobTicket);
        PrintSchema::
        OrientationValues orientationValueD      = defaultJobTicket->PageOrientation->Value;
        JobTicket*        userJobTicket          = new JobTicket(localPrintQueue->UserJobTicket);
        PrintSchema::
        OrientationValues orientationValueU      = userJobTicket->PageOrientation->Value;
        //
        // Print out the queried values
        //
        Console::WriteLine("Local PrintQueue: {0} has the following properites:\n"
                           "Port:           {1}\nDriver:         {2}\nPrintProcessor: {3}\n"
                           "Location:       {4}\nComment:        {5}\nOrientation:    {6}\n"
                           "Description:    {7}\nSeperator_File: {8}\nShare_Name:     {9}\n"
                           "Number Of Jobs  {10}\nPaused:         {11}\n",
                           name,
                           port->Name,driver->Name,printProcessor->Name,
                           queueLocation,queueComment,
                           __box(orientationValueU)->ToString(),
                           description,seperatorFile,share,
                           noJobs,localPrintQueue->IsPaused.ToString());
        //
        // Now lets try changing some of the Local Print Queue Properties
        //
        Port* newPort                            = new Port(newPortName);
        localPrintQueue->QueuePort               = newPort;
        localPrintQueue->Comment                 = new String(S"SystemPrinting PrintQueue");
        localPrintQueue->ShareName               = new String(S"SystemPrinting_Shared");
        localPrintQueue->Priority                = 6;
        localPrintQueue->Name                    = newPrintQueueName;
        userJobTicket->PageOrientation->Value    = PrintSchema::OrientationValues::Landscape;
        localPrintQueue->UserJobTicket           = userJobTicket->XmlStream;
        defaultJobTicket->PageOrientation->Value = PrintSchema::OrientationValues::Landscape;
        localPrintQueue->DefaultJobTicket        = defaultJobTicket->XmlStream;
        //
        // Commit the changes to the PrintQueue
        //
        localPrintQueue->Commit();
        //
        // If the PrintQueue is paused - resume else pause
        //
        if(localPrintQueue->IsPaused)
        {
            Console::WriteLine("Resuming the Print Queue");
            localPrintQueue->Resume();
        }
        else
        {
            Console::WriteLine("Pausing the Print Queue");
            localPrintQueue->Pause();
        }
        //
        // If required, the latest settings could be retrieved from the Print Server once
        // more. This is a step to validate that what we commited is what is currently
        // reflected on the Print Queue
        //
        localPrintQueue->Refresh();
        name                   = localPrintQueue->Name;
        port                   = localPrintQueue->QueuePort;
        priority               = localPrintQueue->Priority;
        queueLocation          = localPrintQueue->Location;
        queueComment           = localPrintQueue->Comment;
        defaultJobTicket       = new JobTicket(localPrintQueue->DefaultJobTicket);
        orientationValueD      = defaultJobTicket->PageOrientation->Value;
        userJobTicket          = new JobTicket(localPrintQueue->UserJobTicket);
        orientationValueU      = userJobTicket->PageOrientation->Value;
        Console::WriteLine("\nReading updated Print Queue information ...");
        Console::WriteLine("Local PrintQueue: {0} has the following properites:\n"
                           "Port:           {1}\n"
                           "Location:       {2}\nComment:        {3}\nOrientation:    {4}\n"
                           "Paused:         {5}\n",
                           name,
                           port->Name,
                           queueLocation,queueComment,
                           __box(orientationValueU)->ToString(),
                           localPrintQueue->IsPaused.ToString());
    }
    //
    // Example of exceptions that can be thrown could be due to
    // access denied while trying to set the Print Queue properties.
    //
    catch(PrintQueueException* printQueueException)
    {
        Console::WriteLine("Error Message: {0}",
                           printQueueException->Message);
    }
    catch (PrintCommitAttributesException* commitException)
    {
        //
        // Handle a PrintCommitAttributesException.
        //
        Console::WriteLine("Error Message: {0}",
                          commitException->Message);
        Console::WriteLine("These attributes failed to commit...");

        IEnumerator* failedAttributesEnumerator = commitException->FailToCommitAttributes->GetEnumerator();

        for (; failedAttributesEnumerator->MoveNext(); )
        {
            String* attributeName = __try_cast<String*>(failedAttributesEnumerator->Current);

            Console::Write("{0}\t", attributeName);
        }

        Console::WriteLine();

        //
        // To have the PrintQueue instance object in sync with the real Print Queue,
        // a refresh could be used. The instance properties that failed to commit will
        // be lost. The caller has the choice to not refresh the failed properties and
        // try another Commit() later one.
        //
        localPrintQueue->Refresh();
    }
    catch(PrintSystemException* printSystemException)
    {
        Console::WriteLine("Error Message: {0}",
                           printSystemException->Message);
    }
    catch(Exception* excpetion)
    {
        Console::WriteLine("Error Message {0}",
                           excpetion->Message);
    }
 }
