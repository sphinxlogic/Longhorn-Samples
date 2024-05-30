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
LocalPrintQueueOperationsWithSubsetOfPropertiesAndCollectionAccess(
    String*     printQueueName
    )
{
    PrintQueue* localPrintQueue = NULL;

    try
    {
        Console::WriteLine("Sample code for Local Print Queue operations");
        //
        // Instantiating an instance of the Local Print Queue with a subset of all
        // possible properties populated. Since we are going to change both PrintQueue
        // and User properties on the PrintQueue, we need Full Access.
        //
        PrintQueueProperty printQueueProperties[] = {PrintQueueProperty::Name,
                                                     PrintQueueProperty::Comment,
                                                     PrintQueueProperty::QueuePort,
                                                     PrintQueueProperty::UserJobTicket};

        localPrintQueue = new PrintQueue(new PrintServer(),
                                         printQueueName,
                                         printQueueProperties,
                                         PrintSystemDesiredAccess::PrinterFullAccess);
        //
        // Querying the name of the PrintQueue
        //

        String*           name                  = __try_cast<String*>(localPrintQueue->
                                                                      PropertiesCollection->
                                                                      Property[S"Name"]->Value);
        //
        // Querying othe properties of the PrintQueue like Comment and Port
        //
        String*           queueComment           = __try_cast<String*>(localPrintQueue->
                                                                      PropertiesCollection->
                                                                      Property[S"Comment"]->Value);
        Port*             port                   = __try_cast<Port*>(localPrintQueue->
                                                                      PropertiesCollection->
                                                                      Property[S"QueuePort"]->Value);
        //
        // Querying the driver settings of the PrintQueue for User
        //
        JobTicket*        userJobTicket          = new JobTicket(__try_cast<Stream*>(localPrintQueue->
                                                                                     PropertiesCollection->
                                                                                     Property[S"UserJobTicket"]->Value));
        PrintSchema::
        OrientationValues orientationValueU      = userJobTicket->PageOrientation->Value;
        //
        // Print out the queried values
        //
        Console::WriteLine("Local PrintQueue: {0} has the following properties:\n"
                           "Comment:        {1}\nOrientation:    {2}\n"
                           "Port:           {3}\n",
                           name,
                           queueComment,
                           __box(orientationValueU)->ToString(),
                           port->Name);
        //
        // Now lets try changing UserJobTicket & Comment
        //
        userJobTicket->PageOrientation->Value    = PrintSchema::OrientationValues::Landscape;
        localPrintQueue->
        PropertiesCollection->
        Property[S"UserJobTicket"]->Value        = userJobTicket->XmlStream;

        localPrintQueue->
        PropertiesCollection->
        Property[S"Comment"]->Value              = new String(S"A new comment");
        //
        // Commit the changes to the PrintQueue
        //
        localPrintQueue->Commit();
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
    catch(Exception* exception)
    {
        Console::WriteLine("Error Message {0}",
                           exception->Message);
    }
 }
