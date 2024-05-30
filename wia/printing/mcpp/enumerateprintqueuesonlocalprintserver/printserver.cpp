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
using namespace Microsoft::Printing::DeviceCapabilities;

#ifndef  __PRINTSERVERSAMPLE_HPP__
#include "PrintServerSample.hpp"
#endif

using namespace SystemPrintingSample;

void
PrintServerSample::
EnumeratePrintQueuesOnLocalPrintServer(
    void
    )
{
    try
    {
        Console::WriteLine("Sample code for enumerating Print Queues on local Print Server\n");

        LocalPrintServer* localPrintServer = new LocalPrintServer();

        PrintQueueCollection* printQueues = localPrintServer->GetPrintQueues();

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
            // Print out the queried property values
            //
            Console::WriteLine("\n--- Printer '{0}' located at '{1}' ---", name, queueLocation);
            Console::WriteLine("Port:        {0}\n"
                               "Driver:      {1}\n"
                               "Comment:     {2}\n"
                               "Shared as:   {3}\n",
                               port->Name,
                               driver->Name,
                               queueComment,
                               shareName);

			//
            // Construct a JobTicket object using the printer's default job ticket.
			//
            JobTicket* jt = new JobTicket(printQueue->DefaultJobTicket);
			//
            // Acquire the printer's device capabilities and construct a DeviceCapabilities object.
			//
            DeviceCapabilities* devcap = new DeviceCapabilities(printQueue->AcquireDeviceCapabilities(printQueue->DefaultJobTicket));
			//
            // Check if the printer can print color.
			//
            Console::WriteLine(devcap->CanPrintColor ?
							   "Can print color documents." :
							   "Cannot print color documents.");

            IEnumerator* option_enumerator;

			//
            // Query for device capability of document duplexing.
			//
            Console::WriteLine("DocumentDuplex Capability:");
            if (devcap->SupportsCapability(PrintSchema::Features::DocumentDuplex))
            {
                option_enumerator = devcap->DocumentDuplexCap->DuplexOptions->GetEnumerator();
                for (;option_enumerator->MoveNext();)
                {
                    DocumentDuplexCapability::DuplexOption* option = __try_cast<DocumentDuplexCapability::DuplexOption*>(option_enumerator->Current);
                    Console::WriteLine("    {0} (constrained by: {1})",
                                       __box(option->Value)->ToString(),
									   __box(option->Constrained)->ToString());
                }
            }
            else
            {
                Console::WriteLine("   not supported");
            }

            // Query for device capability of page resolution.
            Console::WriteLine("PageResolution Capability:");
            if (devcap->SupportsCapability(PrintSchema::Features::PageResolution))
            {
                option_enumerator = devcap->PageResolutionCap->Resolutions->GetEnumerator();
                for (;option_enumerator->MoveNext();)
                {
                    PageResolutionCapability::Resolution* option = __try_cast<PageResolutionCapability::Resolution*>(option_enumerator->Current);
                    Console::WriteLine("    {0}x{1} (constrained by: {2})",
                                       __box(option->ResolutionX)->ToString(),
									   __box(option->ResolutionY)->ToString(),
									   __box(option->Constrained)->ToString());
                }
            }
            else
            {
                Console::WriteLine("not supported");
            }

            // Query for device capability of page canvas size.
            Console::WriteLine("PageCanvasSize Capability:");
            if (devcap->SupportsCapability(PrintSchema::Features::PageCanvasSize))
            {
                // Set the length unit type to Inch in order to get length values in inches.
                devcap->LengthUnitType = PrintSchema::LengthUnitTypes::Inch;

                // If the printer's device capability doesn't specify CanvasSizeX or CanvasSizeY,
                // then the value will be PrintSchema.UnspecifiedDecimalValue.
                if (devcap->PageCanvasSizeCap->CanvasSizeX != PrintSchema::UnspecifiedDecimalValue)
                {
                    Console::WriteLine("    CanvasSizeX = {0}",
						               __box(devcap->PageCanvasSizeCap->CanvasSizeX)->ToString());
                }
                else
                {
                    Console::WriteLine("    CanvasSizeX not specified");
                }

                if (devcap->PageCanvasSizeCap->CanvasSizeY != PrintSchema::UnspecifiedDecimalValue)
                {
                    Console::WriteLine("    CanvasSizeY = {0}",
						               __box(devcap->PageCanvasSizeCap->CanvasSizeY)->ToString());
                }
                else
                {
                    Console::WriteLine("    CanvasSizeY not specified");
                }
            }
            else
            {
                Console::WriteLine("    not supported");
            }

            Console::WriteLine("Printer-default Job Ticket Settings:");
            Console::WriteLine("    PageMediaSize = {0}",
				               __box(jt->PageMediaSize->Value)->ToString());
            Console::WriteLine("    PageOrientation = {0}",
				               __box(jt->PageOrientation->Value)->ToString());
        }
    }
    catch (PrintSystemException* printException)
    {
        Console::WriteLine(printException->Message);
    }
}
