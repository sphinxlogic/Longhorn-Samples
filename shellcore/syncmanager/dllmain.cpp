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


#include "precomp.h"

WCHAR c_szCLSIDDescription[] = L"Sample Synchronization Handler Handler";

//
// Global variables
//

LONG      g_cRefThisDll = 0;    // Reference count of this DLL.
HINSTANCE g_hmodThisDll = NULL; // Handle to this DLL itself.


//+---------------------------------------------------------------------------
//
//  Function:     DllMain, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDAPI_(BOOL) DllMain(HINSTANCE hInstance, DWORD dwReason, void*)
{
    if (dwReason == DLL_PROCESS_ATTACH)
    {
        // Extension DLL one-time initialization
        g_hmodThisDll = hInstance;
        DisableThreadLibraryCalls(hInstance);
    }
    return TRUE;
}


//+---------------------------------------------------------------------------
//
//  Function:     DllRegisterServer, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDAPI DllRegisterServer(void)
{
    WCHAR    szID[GUID_SIZE+1];
    WCHAR    szCLSID[GUID_SIZE+1];
    WCHAR    szModulePath[MAX_PATH];

    // Obtain the path to this module's executable file for later use.
    GetModuleFileName(g_hmodThisDll, szModulePath, ARRAYSIZE(szModulePath));

    // Create some base key strings.
    StringFromGUID2(CLSID_SyncMgrHandler, szID, GUID_SIZE);

    StringCchCopy(szCLSID, ARRAYSIZE(szCLSID), TEXT("CLSID\\"));
    StringCchCat(szCLSID, ARRAYSIZE(szCLSID), szID);

    // Create entries under CLSID.

    SetRegKeyValue(HKEY_CLASSES_ROOT,
        szCLSID,
        NULL,
        c_szCLSIDDescription);

    SetRegKeyValue(HKEY_CLASSES_ROOT,
        szCLSID,
        L"InProcServer32",
        szModulePath);

    AddRegNamedValue(
        szCLSID,
        L"InProcServer32",
        L"ThreadingModel",
        L"Apartment");

    // as this is a sample, we need to set up a few dummy items to sync
    Reg_SetUpRequestedDummySyncItems(3, CLSID_SyncMgrHandler);

    // register with SyncMgr
    ISyncMgrRegister *pSyncMgrRegister;
    HRESULT hr = CoCreateInstance(CLSID_SyncMgr, NULL, CLSCTX_INPROC_SERVER, IID_ISyncMgrRegister, (void**)&pSyncMgrRegister);
    if (SUCCEEDED(hr))
    {
        hr = pSyncMgrRegister->RegisterSyncMgrHandler(CLSID_SyncMgrHandler, c_szCLSIDDescription, 0);
        pSyncMgrRegister->Release();
    }

    return hr;
}


//+---------------------------------------------------------------------------
//
//  Function:     DllUnregisterServer, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDAPI DllUnregisterServer(void)
{
    // UnRegister with SyncMgr
    ISyncMgrRegister *pSyncMgrRegister;
    HRESULT hr = CoCreateInstance(CLSID_SyncMgr, NULL, CLSCTX_INPROC_SERVER, IID_ISyncMgrRegister, (void**)&pSyncMgrRegister);
    if (SUCCEEDED(hr))
    {
        hr = pSyncMgrRegister->UnregisterSyncMgrHandler(CLSID_SyncMgrHandler, 0);
        pSyncMgrRegister->Release();
    }

    // we also need to remove ourselves from HKCR (which is where we store all of our items as well)
    Reg_DeleteHandlerPrefKey(CLSID_SyncMgrHandler);

    return hr;
}

//+---------------------------------------------------------------------------
//
//  Function:     DllCanUnloadNow, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDAPI DllCanUnloadNow(void)
{
    return (g_cRefThisDll == 0) ? S_OK : S_FALSE;
}

//+---------------------------------------------------------------------------
//
//  Function:     DllGetClassObject, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, void **ppvOut)
{
    HRESULT hr = CLASS_E_CLASSNOTAVAILABLE;

    *ppvOut = NULL;

    if (IsEqualIID(rclsid, CLSID_SyncMgrHandler))
    {
        CClassFactory *pcf = new CClassFactory;
        if (pcf)
        {
            hr = pcf->QueryInterface(riid, ppvOut);
            pcf->Release();
        }
        else
        {
            hr = E_OUTOFMEMORY;
        }
    }

    return hr;
}

//+---------------------------------------------------------------------------
//
//  Function:     DllAddRef & DllReleaseRef, public
//
//  Synopsis:
//
//  Arguments:
//
//  Returns:
//
//  Modifies:
//
//----------------------------------------------------------------------------
STDAPI_(void) DllAddRef()
{
    InterlockedIncrement(&g_cRefThisDll);
}

STDAPI_(void) DllRelease()
{
    InterlockedDecrement(&g_cRefThisDll);
}
