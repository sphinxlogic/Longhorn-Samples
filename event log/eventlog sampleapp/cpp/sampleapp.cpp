
#include <windows.h>
#include <stdio.h>
#include <Sddl.h>
#include "SampleAppEvents.h"
#include "mytrace.h"
#include "SampleApp.tmh"
#include "winevt.h"  //Crimson Header. This will be a part of windows.h soon.



#include <tchar.h>
DWORD GetErrorText( DWORD lastError, WCHAR *buff, int buffSize );

int __cdecl wmain(int argc, wchar_t** argv) 
{  
    WPP_INIT_TRACING(L"SampleApp");    

  // Write one event via ReportEvent
  // ===============================
  DoTraceMessage(TRACE_LEVEL_INFORMATION, "Writing an event via ReportEvent (Old API)");

  HANDLE hLog = RegisterEventSourceW(NULL, L"SampleApp");

  if(!hLog)
  {
    DWORD LastErr = GetLastError();
    WCHAR buff[256];
    GetErrorText(LastErr, buff, 256);

    DoTraceMessage(TRACE_LEVEL_ERROR, "RegisterEventSource failed. GetLastError() = 0x%x - %S", LastErr, buff);
    WPP_CLEANUP();
    return 1;
  }

  DoTraceMessage(TRACE_LEVEL_VERBOSE, "RegisterEventSource succeeded");

  LPCWSTR* wszStrings = new LPCWSTR[2];
  wszStrings[0] = L"RED-ITG-PRN-08";

  if(!ReportEventW(
    hLog,
    EVENTLOG_ERROR_TYPE,
    0,        // Category ID
    (DWORD)PrinterConnectionFailure,    //EventID - defined in SampleAppEvents.h
    NULL,      // UserSid
    1,        // # of insertion strings
    0,        // # of raw data bytes
    wszStrings,    // pointer to insertion strings
    NULL))      // pointer to raw data buffer
  {
    DWORD LastErr = GetLastError();
    WCHAR buff[256];
    GetErrorText(LastErr, buff, 256);

    DoTraceMessage(TRACE_LEVEL_ERROR, "ReportEvent failed. GetLastError() = 0x%x - %S", LastErr, buff);
    DeregisterEventSource(hLog);
    WPP_CLEANUP();
    return 1;
  }

  DoTraceMessage(TRACE_LEVEL_VERBOSE, "ReportEvent succeeded");

  DeregisterEventSource(hLog);

  // Write one event via EvtReport (new API)
  // ======================================
  DoTraceMessage(TRACE_LEVEL_INFORMATION, "Writing an event via EvtReport (new API)");

  EVT_HANDLE hPub = NULL;      //Handle to Publisher

  //Register the Publisher
  hPub = EvtRegisterPublisher(  NULL,            //Context
                  GetModuleHandle(NULL),
                  NULL,
                  EvtPublisherManifest    //Flags
                  );
  if(!hPub)
  {  
    DWORD LastErr = GetLastError();
    WCHAR buff[256];
    GetErrorText(LastErr, buff, 256);

    DoTraceMessage(TRACE_LEVEL_ERROR, "EvtRegisterPublisher failed. GetLastError() = 0x%x - %S", LastErr, buff);
    WPP_CLEANUP();
    return 1;
  }

  DoTraceMessage(TRACE_LEVEL_VERBOSE, "EvtRegisterPublisher succeeded");
  
  // If there is existing interest in the event (subscription or log) then build it and raise it
  // otherwise do nothing

  if ( EvtIsActive( hPub, (DWORD)PrinterAdded ) ) 
  {
    EVT_REPORT_STATUS pStatus;  //Out Param which contains the status info.

    //Create the array of Values to be provided. In this case we just have one value
    EVT_VARIANT cEvtArray[1];
    cEvtArray[0].Count = 1; 
    cEvtArray[0].Type = EvtVarTypeString; 
    cEvtArray[0].StringVal = L"RED-ITG-PRN-07";

    BOOL bRet = EvtReport(  hPub,      //hPublisher
                PrinterAdded,      //EventID - defined in SampleAppEvent.h
                NULL,      //EventTemplate - System will decide which template to use based on EventID
                1,        //NumValues
                cEvtArray,    //Values[]
                EvtReportNormal,//Flags
                &pStatus    //Status
                );
    if (!bRet)
    {
      DWORD LastErr = GetLastError();
      WCHAR buff[256];
      GetErrorText(LastErr, buff, 256);

      DoTraceMessage(TRACE_LEVEL_ERROR, "EvtReport failed. GetLastError() = 0x%x - %S", LastErr, buff);

      //Close the Publisher Handle
      EvtClose(hPub);
      WPP_CLEANUP();
      return 1;
    }

    DoTraceMessage(TRACE_LEVEL_VERBOSE, "EvtReport succeeded");
  }
  else
  {
    DoTraceMessage(TRACE_LEVEL_VERBOSE, "EvtIsActive returned False. No interest in the event is there.");
  }


  // Now do the same for the PrinterRemoved event

  if ( EvtIsActive( hPub, (DWORD)PrinterRemoved ) ) 
  {
    EVT_REPORT_STATUS pStatus;  //Out Param which contains the status info.

    //Create the array of Values to be provided. In this case we just have one value
    EVT_VARIANT cEvtArray[1];
    cEvtArray[0].Count = 1; 
    cEvtArray[0].Type = EvtVarTypeString; 
    cEvtArray[0].StringVal = L"RED-ITG-PRN-09";

    BOOL bRet = EvtReport(  hPub,      //hPublisher
                PrinterRemoved,      //EventID - defined in SampleAppEvent.h
                NULL,      //EventTemplate - System will decide which template to use based on EventID
                1,        //NumValues
                cEvtArray,    //Values[]
                EvtReportNormal,//Flags
                &pStatus    //Status
                );
    if (!bRet)
    {
      DWORD LastErr = GetLastError();
      WCHAR buff[256];
      GetErrorText(LastErr, buff, 256);
          
      DoTraceMessage(TRACE_LEVEL_ERROR, "EvtReport failed. GetLastError() = 0x%x - %S", LastErr, buff);
      //Close the Publisher Handle
      EvtClose(hPub);
      WPP_CLEANUP();
      return 1;
    }

    DoTraceMessage(TRACE_LEVEL_VERBOSE, "EvtReport succeeded");
  }
  else
  {
    DoTraceMessage(TRACE_LEVEL_VERBOSE, "EvtIsActive returned False. No interest in the event is there.");
  }

  //Close the Publisher Handle
  EvtClose(hPub);
  printf("\nSuccess" );
    WPP_CLEANUP();

  return 0;
}

DWORD GetErrorText( DWORD lastError, WCHAR *buff, int buffSize )
{
    DWORD retErr = FormatMessageW( FORMAT_MESSAGE_FROM_SYSTEM,
                                   NULL,
                                   lastError,
                                   0,
                                   buff,
                                   buffSize/2,
                                   NULL );

  return retErr;

}



