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
PrintServerOperations(
    String* printServerName,
	String* newSpoolDirectoryName
    )
{
    PrintServer* printServer = NULL;

    try
    {
        PrintServerProperty   printServerProperties[] = {PrintServerProperty::BeepEnabled,
                                                         PrintServerProperty::DefaultSpoolDirectory};
        //
        // The object is created and only the two properties above are initialized.
        // This is a way to optimize in the time required to load the object if only
        // a limited number of properties are required from the object
        //
        printServer = new PrintServer(printServerName,
                                      printServerProperties,
                                      PrintSystemDesiredAccess::AdministrateServer);
        //
        // Read print server properties. These properties are already initialized.
        // This operations doesn't require a call to the Print Spooler service.
        //
        String*   spoolDirectory     = printServer->DefaultSpoolDirectory;
        Boolean   isBeepEnabled      = printServer->BeepEnabled;
        //
        // Read a property that wasn't initialized when the object was constructed.
        // This operation requires a call to to the Print Spooler service. The data
        // will be cached in the printServer object instance.
        //
        System::Threading::ThreadPriority portThreadPriority = printServer->PortThreadPriority;

        //
        // A subsequent call to read this property will use the cached data.
        // This data can change independently by this application. To synchronize
        // printServer properties with the Print Spooler service, the caller must call Refresh().
        //
        Console::WriteLine("Print Server {0} has the following properties:\n"
                           "Spool Directory:     {1}\n"
                           "Beep Enabled:        {2}\n"
                           "Thread Priority:     {3}\n",
                           printServer->Name,
                           spoolDirectory,
                           __box(isBeepEnabled)->ToString(),
                           __box(portThreadPriority)->ToString());

        //
        // Set print server properties.
        //
        printServer->BeepEnabled           = true;
        printServer->DefaultSpoolDirectory = newSpoolDirectoryName;
        //
        // Notice that only properties that whose values changed will be commited.
        // If the printServer object thread property was Normal before the set operation
        // above, then the Commit will operate only for the DefaultSpoolerDirectory property.
        //
        printServer->Commit();
    }
    catch (PrintServerException* serverException)
    {
        //
        // Handle a PrintServerException.
        //
        Console::Write("Server name is {0}. {1} \n",
                       serverException->ServerName,
                       serverException->Message);
    }
    catch (PrintCommitAttributesException* commitException)
    {
        //
        // Handle a PrintCommitAttributesException.
        //
        Console::WriteLine(commitException->Message);
        Console::WriteLine("These attributes failed to commit:");

        IEnumerator* failedAttributesEnumerator = commitException->FailToCommitAttributes->GetEnumerator();

        for (; failedAttributesEnumerator->MoveNext(); )
        {
            String* attributeName = __try_cast<String*>(failedAttributesEnumerator->Current);

            Console::Write("{0}\n", attributeName);
        }
        //
        // To have the printServer object in sync with the Spooler service, a refresh is forced.
        // The properties that failed to commit will be lost.
        // The caller has the choice to not refresh the failed properties and
        // try another Commit() later one.
        //
        printServer->Refresh();
    }
}
