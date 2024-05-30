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


/****************************************************************************
*                                                                           *
* Main.cs - Sample application for Task Scheduler V2 Unmanaged API          *
*                                                                           *
* Component: Task Scheduler                                                 *
*                                                                           *
* Copyright (c) 2002 - 2003, Microsoft Corporation                          *
*                                                                           *
****************************************************************************/


#include <windows.h>
#include <stdio.h>


//Include job header files
#include <jobsitf.h>
#include <jobbuilder.h>
#include <conio.h>



//Create a class for auto releasing COM Pointers
class CReleaseMe
{
protected:
    IUnknown* m_p;

public:
    CReleaseMe( IUnknown* p ) :
	  m_p( p )
	{
	}

    ~CReleaseMe()
	{
		if ( m_p )                      
		{                             
			m_p->Release();             
			m_p = NULL;                 
		}            
	}
};

// Use CBSTR class to automatically allocate/free memory in absence of CBSTR
class CBSTR  
{
    BSTR m_pStr;
public:
    CBSTR() { m_pStr = 0; }
    CBSTR(LPCWSTR pSrc) { m_pStr = SysAllocString(pSrc); }
   ~CBSTR() { if (m_pStr) SysFreeString(m_pStr); }
    operator BSTR() { return m_pStr; }
};


void __cdecl wmain(int argc, wchar_t** argv)
{
	LPCWSTR wszJobName = L"TestJob"; 

	WCHAR wExecutablePath[MAX_PATH];
	if( !GetWindowsDirectoryW( wExecutablePath, MAX_PATH ) )
	{
		printf("\nGetWindowsDirectory failed: %d", GetLastError() );
		return;
	}

        wcsncat( wExecutablePath, L"\\SYSTEM32\\CALC.EXE", MAX_PATH );

	//The task will be created to run as this user with this password.
	//This should be updated to the correct user account present in the system

	if( argc < 2 )
	{
		printf("\nUsage: CS_Sample1 <UserName> " );
		return;
	}

	LPCWSTR wszUserName = argv[1];
	WCHAR wPassword[MAX_PATH];
	WCHAR* wPOrg = wPassword;
	WCHAR * wNi = wPassword;
	//Get the password from the console
	printf("\nEnter the Password for the user %S : ", argv[1] );

	while( wchar_t wTemp = _getwch() )
	{
		if( wTemp == '\r' )
			break;
		*wPOrg = wTemp;
		wPOrg++;
	}
	HRESULT hr = CoInitializeEx(NULL, COINIT_MULTITHREADED);
	if( FAILED(hr) )
	{
		printf("\nCoInitializeEx failed: %x", hr );
		return;
	}

	{

	hr = CoInitializeSecurity(
				NULL,
				-1,
				NULL,
				NULL,
				RPC_C_AUTHN_LEVEL_PKT,
				RPC_C_IMP_LEVEL_IMPERSONATE,
				NULL,
				0,
				NULL);

	if( FAILED(hr) )
	{
		printf("\nCoInitializeSecurity failed: %x", hr );
		return;
	}

	//Instantiate Job Service 

    IJobService *pService = NULL;
    hr = CoCreateInstance( CLSID_JobService,
                           NULL,
                           CLSCTX_LOCAL_SERVER,
                           IID_IJobService,
                           (void**)&pService );  
    if (FAILED(hr))
    {
          printf("Failed to CoCreate a instance of the JobService class: %x", hr);
          return;
    }
    //Make sure pointer is released when it goes out of scope
	CReleaseMe cJobService( pService );
        
    //Get the pointer to root folder 
	IJobFolder *pRootFolder = NULL;
	hr = pService->GetFolder( CBSTR( L"\\") , &pRootFolder );
	if( FAILED(hr) )
	{
		printf("Cannot get Root Folder Pointer: %x", hr );
		return;
	}

	//Make sure pointer is released when it goes out of scope
	CReleaseMe cJobFolder( pRootFolder );


    //Check if same job already exist. If exist, then remove it
	VARIANT_BOOL varBool = VARIANT_FALSE;

    hr = pService->HasJob( CBSTR( wszJobName ), &varBool);
	if( SUCCEEDED(hr) && varBool == VARIANT_TRUE )
            hr = pRootFolder->RemoveJob( CBSTR( wszJobName)  );

    if( FAILED(hr) )
    {
		printf("\nFailed to Remove existing job: %x", hr );
		return;
    }


	// Create job builder object
	IJobBuilder *pJobBuilder = NULL;

    hr = CoCreateInstance( CLSID_JobBuilder,
                           NULL,
                           CLSCTX_INPROC,
                           IID_IJobBuilder,
                           (void**)&pJobBuilder );  
    if (FAILED(hr))
    {
          printf("Failed to CoCreate a instance of the JobService class: %x", hr);
          return;
    }
    //pJobBuilder->Release();
	//Make sure pointer is released when it goes out of scope
	CReleaseMe cJobBuilder( pJobBuilder );
	
	//First add a step to run calc.exe		
	IExecutableStep *pExecStep = NULL;
	IStepCollection *pStepCollection = NULL;

	// Get the Step collection pointer
	hr = pJobBuilder->get_Steps( &pStepCollection );
	if( FAILED(hr) )
	{
		printf("\nCannot get Step collection pointer: %x", hr );
		return;
	}

	//Make sure pointer is released when it goes out of scope
	CReleaseMe cCollection( pStepCollection );
	
	IJobStep *pJobStep = NULL;
	hr = pStepCollection->Add( ExecutableStep, &pJobStep );

	//QI for Executable step pointer
	if( SUCCEEDED(hr) )
		hr = pJobStep->QueryInterface( IID_IExecutableStep, (void**) &pExecStep );

	//Make sure pointer is released when it goes out of scope
	CReleaseMe cExecStep( pExecStep );
	CReleaseMe cJobStep( pJobStep );

	//Set the path of the executable to calc.exe
	hr = pExecStep->put_Path( CBSTR( wExecutablePath ) );	 
	if( FAILED(hr) )
	{
		printf("\nCannot set path of executable: %x", hr );
		return;
	}


	// Create a time trigger to start the job in one minute
	SYSTEMTIME SysTime;
	GetLocalTime( &SysTime );

	SysTime.wMinute += 1;
	if( SysTime.wMinute > 59 )
		SysTime.wHour += 1;
	if( SysTime.wHour > 24 )
		SysTime.wDay += 1;
	// We are not dealing with month/year change here.

	WCHAR wTime[MAX_PATH];
	
	swprintf(	wTime, 
				L"%04d-%02d-%02dT%02d:%02d:%02d",
				SysTime.wYear,
				SysTime.wMonth,
        		SysTime.wDay,
				SysTime.wHour,
        		SysTime.wMinute,
				SysTime.wSecond  );

	printf("\nSetting StartTime as %S ", wTime );
	
	ITriggerCollection *pTriggerCollection = NULL;

	hr = pJobBuilder->get_Triggers( &pTriggerCollection );
	if( FAILED(hr) )
	{
		printf("\nCannot get TriggerCollection Ptr:  %x", hr );
		return;
	}

	//Now add the timetrigger to the job
	IJobTrigger *pJobTrigger = NULL;
	ITimeTrigger *pTimeTrigger = NULL;
	hr = pTriggerCollection->Add( Time, &pJobTrigger );		
	if( SUCCEEDED(hr) )
		hr = pJobTrigger->QueryInterface( IID_ITimeTrigger, (void**) &pTimeTrigger );
	if( SUCCEEDED(hr) )
		hr = pTimeTrigger->put_StartTime( CBSTR(wTime) );

	//Make sure pointer is released when it goes out of scope
	CReleaseMe cJobTrigger( pJobTrigger );
	CReleaseMe cTimeTrigger( pTimeTrigger );

	if( FAILED(hr) )
	{
		printf("\nCannot add time trigger to the job %x", hr );
		return;
	}
	
	hr = pJobBuilder->put_UserId( CBSTR( wszUserName ) );
	if( FAILED(hr) )
	{
		printf("\nError saving the job : %x", hr );
		return;
	}

	hr = pJobBuilder->put_Password( CBSTR( wPassword ) );
	if( FAILED(hr) )
	{
		printf("\nError saving the job : %x", hr );
		return;
	}
	
	//Now save the job in the root folder
	hr = pJobBuilder->Save( CBSTR( wszJobName) , CBSTR( L"/") );
	if( FAILED(hr) )
	{
		printf("\nError saving the job : %x", hr );
		return;
	}

	printf("\n Success !!! Job succesfully registered " );
	}

	CoUninitialize();

}

