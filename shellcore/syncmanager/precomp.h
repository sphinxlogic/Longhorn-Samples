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


#include <objbase.h>
#include <windows.h>
#include <shlobj.h>
#include <commctrl.h>
#include <mobsync.h>
#include <shlobj.h>
#include <shlwapi.h>
#include <strsafe.h>
#include "resource.h"

#ifndef ARRAYSIZE
#define ARRAYSIZE(x)        (sizeof(x) / sizeof(x[0]))
#endif

#define SHAnsiToUnicode(psz, pwsz, cchwsz)  MultiByteToWideChar(CP_ACP, 0, psz, -1, pwsz, cchwsz);
#define SHUnicodeToAnsi(pwsz, psz, cchsz)   WideCharToMultiByte(CP_ACP, 0, pwsz, -1, psz, cchsz, NULL, NULL);

STDAPI_(void) DllAddRef(void);
STDAPI_(void) DllRelease(void);


#include "cfact.h"
#include "enum.h"
#include "handler.h"
#include "reg.h"
#include "guid.h"


#define GUID_SIZE 128


//
// Global variables
//

EXTERN_C HINSTANCE g_hmodThisDll; // Handle to this DLL itself.
EXTERN_C const GUID CLSID_SyncMgrHandler;


#pragma hdrstop
